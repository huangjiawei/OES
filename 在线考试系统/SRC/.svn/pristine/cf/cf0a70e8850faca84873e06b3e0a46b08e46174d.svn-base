using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineExam.Models;
using System.Web.Mvc;
namespace OnlineExam
{
    public class JsonPaser
    {
        ExamEntities ee = new ExamEntities();
        public string GetSpan(string id, string css, string content, string draggable = "false", string otherAttr = "")
        {
            string elem = "<span id='{0}' class='{1}' draggable='{2}' {3}>{4}</span>";
            return string.Format(elem, id, css, draggable, otherAttr, content);
        }
        //public List<BsTreeNode> GetSubjectTree(out List<Major> majors, out List<Subject> subjects)
        //{

        //    List<BsTreeNode> nodes = new List<BsTreeNode>();
        //    BsTreeNode tree = new BsTreeNode();
        //    tree.text = GetSpan("0", "tree-top", "专业分类", "false");
        //    majors = ee.Major.OrderBy(m => m.MajorName).ToList();
        //    subjects = ee.Subject.OrderBy(m => m.SubjectName).ToList();
        //    var mss = ee.Major_Subject.ToList();
        //    foreach (var m in majors)
        //    {
        //        BsTreeNode n = new BsTreeNode();
        //        n.text = GetSpan(m.MajorID.ToString(), "tree-major", m.MajorName);

        //        n.tags.Add(m.MajorID.ToString());
        //        n.tags.Add("Major");
        //        var subSubjects = from s in subjects
        //                          from r in mss
        //                          where r.SubjectID == s.SubjectID && r.MajorID == m.MajorID
        //                          select s;

        //        foreach (var sub in subSubjects)
        //        {

        //            BsTreeNode n1 = new BsTreeNode();
        //            n1.text = GetSpan(sub.SubjectID.ToString(), "tree-subject", sub.SubjectName);
        //            n1.tags.Add(sub.SubjectID.ToString());
        //            n1.tags.Add("Subject");
        //            n1.tags.Add(sub.SubjectCode);
        //            n1.backColor = "antiquewhite";
        //            n.nodes.Add(n1);
        //        }
        //        tree.nodes.Add(n);
        //    }
        //    nodes.Add(tree);
        //    return nodes;
        //}

        public string GetSubjectTree(out List<Major> majors, out List<Subject> subjects)
        {

            List<easyUiTreeNode> nodes = new List<easyUiTreeNode>();
            easyUiTreeNode tree = new easyUiTreeNode();
            tree.text = GetSpan("0","tree-top","专业分类");
            tree.id = "0";
            //tree.iconCls = "tree-top";
            majors = ee.Major.OrderBy(m => m.MajorName).ToList();
            subjects = ee.Subject.OrderBy(m => m.SubjectName).ToList();
            var mss = ee.Major_Subject.ToList();
            foreach (var m in majors)
            {
                easyUiTreeNode n = new easyUiTreeNode();
                n.text = GetSpan(m.MajorID.ToString(), "tree-major",m.MajorName);
                //n.iconCls = " glyphicon-paperclip";
                //n.iconCls = "tree-major";
                n.id = m.MajorID.ToString();
                n.attributes = new { NodeType = "Major", NodeId = n.id };
                //n.tags.Add("Major");
                var subSubjects = from s in subjects
                                  from r in mss
                                  where r.SubjectID == s.SubjectID && r.MajorID == m.MajorID
                                  select s;

                foreach (var sub in subSubjects)
                {

                    easyUiTreeNode n1 = new easyUiTreeNode();
                    n1.text =GetSpan(sub.SubjectID.ToString(),"tree-subject", sub.SubjectName);
                    n1.id ="subject"+ sub.SubjectID.ToString();
                    //n1.iconCls = " glyphicon-leaf";
                    //n1.iconCls = "tree-subject";
                    n1.attributes = new { NodeType = "Subject", NodeId = n1.id };

                    //n1.attributes = "{SubjectCode:}";
                    //n1.backColor = "antiquewhite";
                    n.children.Add(n1);
                }
                tree.children.Add(n);
            }
            nodes.Add(tree);
            string result = System.Web.Helpers.Json.Encode(nodes);
            return result.Replace(",\"children\":[]", "");
        }

    }

    public class easyUiTreeNode
    {
        public string id = "";
        public string text = "";
        public string iconCls = "";// glyphicon glyphicon-list
        //public string state = "open";//：节点状态，'open' or 'closed'，默认为'open'。当设置为'closed'时，拥有子节点的节点将会从远程站点载入它们。
        //public string checked_ = "";//：表明节点是否被选择。
        public object attributes=new { };//：可以为节点添加的自定义属性。
        public List<easyUiTreeNode> children = new List<easyUiTreeNode>();//：子节点

    }

    public class NodeState
    {
        bool check = true;
        bool disabled = true;
        bool expanded = true;
        bool selected = true;
    }
    public class BsTreeNode
    {
        public string text = "";
        public string icon = "";
        public string selectedIcon = "";
        public string color = "#000000";
        public string backColor = "#FFFFFF";
        //public string href = "#node-1";
        //public bool selectable = true;
        //public NodeState state = new NodeState();
        public List<string> tags = new List<string>();
        public List<BsTreeNode> nodes = new List<BsTreeNode>();
    }

}
