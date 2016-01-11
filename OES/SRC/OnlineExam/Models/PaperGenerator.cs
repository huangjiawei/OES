using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace OnlineExam.Models
{
    public class PaperGenerate
    {
        ExamEntities ee = new ExamEntities();
        public TestPaper Paper
        {
            get; set;
        }
        public double Score_Choice(Paper_AnswerList list)
        {
            var cates = Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.SingleChoice.ToString() || QuestionType.MultipleChoice.ToString() == m.QuestionType);
            var pc = from s in cates
                     select s.Paper_Choice;
            List<Paper_Choice> pcs = new List<Paper_Choice>();//获取所有选择选题
            foreach (var p in pc)
            {
                pcs.AddRange(p.AsEnumerable());
            }
            var answerItemList = list.Paper_AnswerList_Item;
            bool a, b, c, d, e, f;
            double sum = 0.0;
            foreach (var t in pcs)
            {
                a = b = c = d = e = f = false;
                var question = Choice.Where(m => m.ID == t.QuestionID).Single();
                var answerItem = answerItemList.Where(m => m.SmallQuestionNumber == t.SmallQuestionNumber && m.BigQuestionNumber == t.BigQuestionNumber).Single();
                if (string.IsNullOrWhiteSpace(answerItem.Answer)) { answerItem.IsCorrect = false; answerItem.Score = 0; }
                else
                {
                    if (answerItem.Answer.IndexOf('A') >= 0 || answerItem.Answer.IndexOf('a') >= 0) a = true;
                    if (answerItem.Answer.IndexOf('B') >= 0 || answerItem.Answer.IndexOf('b') >= 0) b = true;
                    if (answerItem.Answer.IndexOf('C') >= 0 || answerItem.Answer.IndexOf('c') >= 0) c = true;
                    if (answerItem.Answer.IndexOf('D') >= 0 || answerItem.Answer.IndexOf('d') >= 0) d = true;
                    if (answerItem.Answer.IndexOf('E') >= 0 || answerItem.Answer.IndexOf('e') >= 0) e = true;
                    if (answerItem.Answer.IndexOf('F') >= 0 || answerItem.Answer.IndexOf('f') >= 0) f = true;
                    if (question.AisTrue == a && question.BisTrue == b
                        && question.CisTrue == c
                        && question.DisTrue == d
                        && question.EisTrue == e
                        && question.FisTrue == f)
                    { answerItem.IsCorrect = true; answerItem.Score = t.Score; sum += t.Score; }
                    else { answerItem.IsCorrect = false; answerItem.Score = 0; }
                }
            }
            return sum;
            //}
        }
        public Paper_AnswerList GenerateAnswerList(string[] answers)
        {
            var answerlist = new Paper_AnswerList
            {
                PaperID = Paper.ID,
                StudentID = SessionHelper.UserProfile.UserId
            };
            int answers_index = 0;
            foreach (var cate in Paper.Paper_QuestionCategory.Where(m => m.Quantity > 0).OrderBy(m => m.Sequence))
            {
                var count = 0;
                if (cate.QuestionType == QuestionType.SingleChoice.ToString() || cate.QuestionType == QuestionType.MultipleChoice.ToString())
                {
                    
                    //count = cate.Paper_Essay.Count();
                    count = cate.Paper_Choice.Count();
                }
                else count = cate.Paper_Essay.Count();
                if (count > 0)
                {
                    int smallQuestionNumber = 0;
                    var query = from s in answers.Skip(answers_index).Take(count)
                                select new Models.Paper_AnswerList_Item
                                {
                                    Answer = s,
                                    BigQuestionNumber = cate.Sequence,
                                    SmallQuestionNumber = ++smallQuestionNumber
                                };
                    foreach (var item in query)
                    {
                        answerlist.Paper_AnswerList_Item.Add(item);
                    }
                    answers_index += smallQuestionNumber;
                
                }
            }
            return answerlist;
        }
 
        public void DefaultSetting()
        {
            Paper.ExamTime = DateTime.Now.AddMinutes(1);
            Paper.ExamType = ExamType.练习题.ToString();
            Paper.Name = "练习题";
            Paper.SubjectID = 0;
            Paper.TimeDuration = 120;
            Paper.Active = Activity.Active;
            Paper.Audit = Auditing.Unreviewed;
            Paper.Info = "Generate by OnlineExam System";
            Paper.IsReady = true;
            Paper.ModificationDate = DateTime.Now;
            Paper.ModificationTeacher = "System";
            Paper.ModificationTeacherID = "System";
            Paper.Paper_QuestionCategory.Add(new Paper_QuestionCategory(Paper.ID, 1, QuestionType.SingleChoice, 1, 20));
            Paper.Paper_QuestionCategory.Add(new Paper_QuestionCategory(Paper.ID, 2, QuestionType.MultipleChoice, 4, 1));
            Paper.Paper_QuestionCategory.Add(new Paper_QuestionCategory(Paper.ID, 3, QuestionType.Completion, 4, 10));
            Paper.Paper_QuestionCategory.Add(new Paper_QuestionCategory(Paper.ID, 4, QuestionType.ShortAnswer, 2,30));
            Paper.Paper_QuestionCategory.Add(new Paper_QuestionCategory(Paper.ID, 5, QuestionType.Completion, 10, 10));
            Paper.Paper_QuestionCategory.Add(new Paper_QuestionCategory(Paper.ID, 6, QuestionType.SingleChoice, 2, 11));

        }
        
        public List<QuestionChoice> Choice
        {
            get; set;
        }
        public List<QuestionEssay> Essay { get; set; }
        public PaperGenerate()
        {
            Paper = new TestPaper();
            Paper.ID = Guid.NewGuid().ToString();
        }
     
        public void AutoGenerate()
        {
            ExamEntities ee = new ExamEntities();
            Choice = new List<QuestionChoice>();
            Essay = new List<QuestionEssay>();
            //随机抽取试题
#if DEBUG
            //不做科目筛选
            var choiceids = ee.QuestionChoice.Select(m => m.ID);
            var essayids = ee.QuestionEssay.Select(m => m.ID);
#else
            var choiceids = ee.Subject_Choice.Where(m => m.SubjectID == Paper.SubjectID).Select(m => m.QuestionID);
            var essayids = ee.Subject_Essay.Where(m => m.SubjectID == Paper.SubjectID).Select(m => m.QuestionID);
#endif
            var singlechoice = (from s in ee.QuestionChoice
                                where s.IsMultiple == false && choiceids.Contains(s.ID)
                                orderby Guid.NewGuid()
                                select s).Take(
                Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.SingleChoice.ToString()).Select(m => m.Quantity).Sum())
                .ToList();
            var multiplechoice = (from s in ee.QuestionChoice
                                  where s.IsMultiple == true && choiceids.Contains(s.ID)
                                  orderby Guid.NewGuid()
                                  select s).Take(
                Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.MultipleChoice.ToString()).Select(m => m.Quantity).Sum())
                .ToList();
            Choice.AddRange(singlechoice.Union(multiplechoice));
            var shortanswer = (from s in ee.QuestionEssay
                               where s.QuestionType == QuestionType.ShortAnswer.ToString() && essayids.Contains(s.ID)
                               orderby Guid.NewGuid()
                               select s).Take(Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.ShortAnswer.ToString()).Select(m => m.Quantity).Sum()
                               ).ToList();
            var completion = (from s in ee.QuestionEssay
                              where s.QuestionType == QuestionType.Completion.ToString() && essayids.Contains(s.ID)
                              orderby Guid.NewGuid()
                              select s).Take(
                Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.Completion.ToString()).Select(m => m.Quantity).Sum())
                .ToList();
            var discussion = (from s in ee.QuestionEssay
                              where s.QuestionType == QuestionType.Discussion.ToString() && essayids.Contains(s.ID)
                              orderby Guid.NewGuid()
                              select s).Take(
                Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.Discussion.ToString()).Select(m => m.Quantity).Sum())
                .ToList();
            Essay.AddRange(shortanswer.Union(completion).Union(discussion));
            //添加与试卷的关联
            int choice_index = 0;
            foreach (var bq in Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.SingleChoice.ToString()))
            {
                if (singlechoice.Count > choice_index)
                {
                    int smallQNumber = 1;//小题号
                    var qlist = singlechoice.Skip(choice_index).Take(bq.Quantity);
                    var temp = from s in qlist
                               select new Paper_Choice
                               {
                                   PaperID = Paper.ID,
                                   QuestionID = s.ID,
                                   Score = bq.ScorePerQuestion
                               ,
                                   BigQuestionNumber = bq.Sequence,
                                   SmallQuestionNumber = smallQNumber++
                               };
                    if (bq.Paper_Choice == null) bq.Paper_Choice = new List<Paper_Choice>();
                    ((List<Paper_Choice>)bq.Paper_Choice).AddRange(temp);
                   
                    var count = temp.Count();
                    bq.Quantity = count;
                    choice_index += count;
                }
            }
            choice_index = 0;
            foreach (var bq in Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.MultipleChoice.ToString()))
            {
                if (multiplechoice.Count > choice_index)
                {
                    int smallQNumber = 1;//小题号
                    var qlist = multiplechoice.Skip(choice_index).Take(bq.Quantity);
                    var temp = from s in qlist
                               select new Paper_Choice
                               {
                                   PaperID = Paper.ID,
                                   QuestionID = s.ID,
                                   Score = bq.ScorePerQuestion
                               ,
                                   BigQuestionNumber = bq.Sequence,
                                   SmallQuestionNumber = smallQNumber++
                               };
                    if (bq.Paper_Choice == null) bq.Paper_Choice = new List<Paper_Choice>();
                    ((List<Paper_Choice>)bq.Paper_Choice).AddRange(temp);
                    var count = temp.Count();
                    bq.Quantity = count;
                    choice_index += count;
                }
       
            }
            int essay_index = 0;
            foreach (var bq in Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.Completion.ToString()))
            {
                if (completion.Count > essay_index)
                {
                    int smallQNumber = 1;//小题号
                    var qlist = completion.Skip(essay_index).Take(bq.Quantity);
                    var temp = from s in qlist
                               select new Paper_Essay
                               {
                                   PaperID = Paper.ID,
                                   QuestionID = s.ID,
                                   Score = bq.ScorePerQuestion
                               ,
                                   BigQuestionNumber = bq.Sequence,
                                   SmallQuestionNumber = smallQNumber++
                               };
                    if (bq.Paper_Essay == null) bq.Paper_Essay = new List<Paper_Essay>();
                    ((List<Paper_Essay>)bq.Paper_Essay).AddRange(temp);
                    var count = temp.Count();
                    bq.Quantity = count;
                    essay_index += count;
                }
   
            }
            essay_index = 0;
            //smallQuestionNumber = 1;
            foreach (var bq in Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.ShortAnswer.ToString()))
            {
                if (shortanswer.Count > essay_index)
                {
                    int smallQNumber = 1;//小题号
                    var qlist = shortanswer.Skip(essay_index).Take(bq.Quantity);
                    var temp = from s in qlist
                               select new Paper_Essay
                               {
                                   PaperID = Paper.ID,
                                   QuestionID = s.ID,
                                   Score = bq.ScorePerQuestion
                               ,
                                   BigQuestionNumber = bq.Sequence,
                                   SmallQuestionNumber = smallQNumber++
                               };
                    if (bq.Paper_Essay == null) bq.Paper_Essay = new List<Paper_Essay>();
                    ((List<Paper_Essay>)bq.Paper_Essay).AddRange(temp);
                    var count = temp.Count();
                    bq.Quantity = count;
                    essay_index += count;
                }
        
            }
            essay_index = 0;
            //smallQuestionNumber = 1;
            foreach (var bq in Paper.Paper_QuestionCategory.Where(m => m.QuestionType == QuestionType.Discussion.ToString()))
            {
                if (discussion.Count > essay_index)
                {
                    int smallQNumber = 1;//小题号
                    var qlist = discussion.Skip(essay_index).Take(bq.Quantity);
                    var temp = from s in qlist
                               select new Paper_Essay
                               {
                                   PaperID = Paper.ID,
                                   QuestionID = s.ID,
                                   Score = bq.ScorePerQuestion
                               ,
                                   BigQuestionNumber = bq.Sequence,
                                   SmallQuestionNumber = smallQNumber++
                               };
                    if (bq.Paper_Essay == null) bq.Paper_Essay = new List<Paper_Essay>();
                    ((List<Paper_Essay>)bq.Paper_Essay).AddRange(temp);
                    var count = temp.Count();
                    bq.Quantity = count;
                    essay_index += count;
                }
        
            }
            ee.TestPaper.Add(Paper);
            ee.SaveChanges();
        }
    }
}