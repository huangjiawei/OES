using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineExam.Models;
using System.Collections;
using System.Collections.Generic;
using MvcContrib.UI.Grid;
using System.Web.Security;
namespace OnlineExam.Controllers
{
    
    [Authorize]
    public partial class AccountController : Controller
    {
        
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AccountController()
        {
        }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        IEnumerable<SelectListItem> _roleList;
        void InitRoles()
        {
            var temp = new SelectList(ee.AspNetRoles, "Name", "DisplayName").ToList();
            SelectListItem i = new SelectListItem();
            i.Value = "all";
            i.Text = "全部";
            temp.Insert(0, i);
            _roleList = temp.AsEnumerable();
        }
        public IEnumerable<SelectListItem> RoleList
        {
            get
            {
                if (_roleList == null) InitRoles();
                return _roleList;
            }
        }
        int pageSize = CKey.DefaultPageCount;
#if !DEBUG
        [Authorize(Roles = "Admin")]
#endif
        public ActionResult UserManagement(string searchWord, int? pageSize, GridSortOptions gridSortOptions, int? page, string SelectRole, DataView? viewType)
        {
            ViewBag.Roles = RoleList;
            ViewData["ViewType"] = viewType;
            int CurrentPageSize = 0;
            if (!pageSize.HasValue)
                CurrentPageSize = (viewType == DataView.GridView ? CKey.GridViewPageCount : CKey.DataListPageCount);
            else CurrentPageSize = pageSize.Value;
            var query = from u in ee.AspNetUsers
                        from p1 in (from p in ee.UserProfile where p.UserId == u.UserName select p).DefaultIfEmpty()
                            //from r1 in                      
                            //from r1 in (from r in ee.AspNetRoles )
                        select new UserDetail
                        {
                            Profile = p1,
                            UserName = u.UserName,
                            Email = u.Email,
                            UserId = u.Id,
                            Roles = (from r in ee.AspNetRoles
                                     from ur in u.AspNetRoles
                                     where ur.Id == r.Id
                                     select r).ToList()
                        };
            var pagedViewModel = new PagedViewModel<UserDetail>
            {
                ViewData = ViewData,
                Query = query
                //from u in ee.AspNetUsers
                //from p in ee.UserProfile.DefaultIfEmpty()
                //where u.UserName == p.UserId
                //select new UserDetail { Profile = p, UserName = u.UserName, Email = u.Email }
                ,
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "UserName",
                Page = page,
                PageSize = CurrentPageSize
            }
            .AddFilter("searchWord", searchWord, a => a.UserName.Contains(searchWord) || a.Profile.NickName.Contains(searchWord) || a.Profile.RealName.Contains(searchWord) || a.Email.Contains(searchWord))
            //.AddFilter("genreId", genreId, a => a.GenreId == genreId, _service.GetGenres(), "Name")
            //.AddFilter("artistId", artistId, a => a.ArtistId == artistId, _service.GetArtists(), "Name")
        ;
            if (!string.IsNullOrWhiteSpace(SelectRole) && SelectRole != "all")
            {
                pagedViewModel.AddFilter("SelectRole", SelectRole, m => m.Roles.Select(u => u.Name).Contains(SelectRole));
            }
            pagedViewModel.Setup();
            //ViewData = pagedViewModel.ViewData;
            return View(pagedViewModel);
        }
#if !DEBUG
        [Authorize(Roles = "Admin")]
#endif
        public ActionResult RoleAdmin(string name)
        {
            var user = UserManager.Users.Where(m => m.UserName == name).SingleOrDefault();
            var profile = ee.UserProfile.Where(m => m.UserId == name).SingleOrDefault(); ;
            if (user == null) return HttpNotFound();
            ViewBag.UserName = user.UserName;
            ViewBag.ID = user.Id;
            ViewBag.Email = user.Email;
            ViewBag.UserRole = UserManager.GetRoles(user.Id);
            if (profile == null)
            {
                ViewBag.HasProfile = false;
            }
            else
            {
                ViewBag.HasProfile = true;
            }
            ViewBag.Profile = profile;
            var list = RoleList.ToList();
            list.RemoveAt(0);
            ViewBag.Roles = list;
            return View();
        }
#if !DEBUG
        [Authorize(Roles = "Admin")]
#endif
        [HttpPost]
        public JsonResult SetUserRole()
        {
            string UserId = Request.Params["UserId"];
            string UserRoles = Request.Params["UserRoles"];
            JsonReturn jr = new JsonReturn();
            try
            {
                var allRoles = RoleList.Select(m => m.Value).ToList().ToArray();
                //删除用户的所有角色
                ee.AspNetUsers.Where(m => m.Id == UserId).Single().AspNetRoles.Clear();
                ee.SaveChanges();
                //重置角色
                if (!string.IsNullOrEmpty(UserRoles)) UserManager.AddToRoles(UserId, UserRoles.Split(','));
                jr.Success = 1;
            }
            catch (Exception ex)
            {
                jr.Success = 0;
                jr.Message = ex.Message;
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        void AppendSubjectToTeacher(string sids, string teacher)
        {
            ExamEntities ee = new ExamEntities();
            var temp = sids.Split(',');
            List<int> ids = new List<int>();
            List<Teacher_Subject> ses = new List<Teacher_Subject>();
            foreach (var i in temp)
            {
                ids.Add(int.Parse(i.Trim()));
                var t = new Teacher_Subject();
                t.SubjectID = int.Parse(i.Trim());
                t.TeacherID = teacher;
                ses.Add(t);
            }
            ee.Teacher_Subject.RemoveRange(ee.Teacher_Subject.Where(m => m.TeacherID == teacher));
            //string sql = "delete Subject_Essay where QuestionId={0};insert into Subject_Essay(subjectid,questionid) values {2}"; 
            //sql=string.Format(sql,qid)
            ee.Teacher_Subject.AddRange(ses.AsEnumerable());
            ee.SaveChanges();
        }
#if !DEBUG
        [Authorize(Roles = "Admin")]
#endif
        [HttpPost]
        public JsonResult SetTeacherSubject()
        {
            string UserId = Request.Params["UserId"];
            string ids = Request.Params["SubjectIds"];
            string sids = Request.Params["SubjectIds"];
            if (sids != null && sids.Length > 0)
                sids = sids.Substring(1);
           
            //var id = ee.AspNetUsers.Where(m => m. == UserId).Select(m => m.Id).Single();
            bool ok = UserManager.IsInRole(UserId, "Teacher") | UserManager.IsInRole(UserId, "Editor") || UserManager.IsInRole(UserId, "Audit");
            JsonReturn jr = new JsonReturn();
            if (ok)
            {
                try
                {
                    var name = UserManager.FindById(UserId).UserName;
                    AppendSubjectToTeacher(sids, name);
                    jr.Success = 1;
                }
                catch (Exception ex)
                {
                    jr.Success = 0;
                    jr.Message = ex.Message;
                }
            }
            else
            {
                jr.Success = 0;
                jr.Message = "非法操作，当前用户无权管理角色";
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
#if !DEBUG
        [Authorize(Roles = "Admin")]
#endif
        [HttpPost]
        public JsonResult DeleteUser(string name)
        {
       
            JsonReturn jr = new JsonReturn();
            try
            {
                UserManager.Delete(UserManager.FindByName(name));
                jr.Success = 1;
            }
            catch (Exception ex)
            {
                jr.Success = 0;
                jr.Message = ex.Message;
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
#if !DEBUG
        [Authorize(Roles = "Admin")]
#endif
        public ActionResult TeacherSubjectAdmin(string name)
        {
            var user = UserManager.Users.Where(m => m.UserName == name).SingleOrDefault();
            var profile = ee.UserProfile.Where(m => m.UserId == name).SingleOrDefault(); ;
            if (user == null) return HttpNotFound();
            ViewBag.UserName = user.UserName;
            ViewBag.ID = user.Id;
            ViewBag.Email = user.Email;
            ViewBag.UserRole = UserManager.GetRoles(user.Id);
            if (profile == null)
            {
                ViewBag.HasProfile = false;
            }
            else
            {
                ViewBag.HasProfile = true;
            }
            ViewBag.Profile = profile;
            var list = (from s in ee.Subject
                        from ts in ee.Teacher_Subject.Where(m => m.TeacherID == name)
                        where ts.SubjectID == s.SubjectID
                        select s).ToList();
            //ee.Teacher_Subject.Where(m => m.TeacherID == name).ToList();
            ViewBag.Subjects = list;
            return View();
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        ExamEntities ee = new ExamEntities();
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserID, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    //var user = model.UserID;
                    //SessionHelper.UserProfile = ee.UserProfile.Where(m => m.UserId == user).SingleOrDefault();
                    //if (SessionHelper.UserProfile==null) 
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "无效的登录尝试。");
                    return View(model);
            }
        }
        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public virtual async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // 要求用户已通过使用用户名/密码或外部登录名登录
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }
        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // 以下代码可以防范双重身份验证代码遭到暴力破解攻击。
            // 如果用户输入错误代码的次数达到指定的次数，则会将
            // 该用户帐户锁定指定的时间。
            // 可以在 IdentityConfig 中配置帐户锁定设置
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "代码无效。");
                    return View(model);
            }
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Name, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // 有关如何启用帐户确认和密码重置的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=320771
                    // 发送包含此链接的电子邮件
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "确认你的帐户", "请通过单击 <a href=\"" + callbackUrl + "\">這裏</a>来确认你的帐户");
                    return RedirectToAction("EditProfile");
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public virtual async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return View();
        }
        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // 请不要显示该用户不存在或者未经确认
                    return View("ForgotPasswordConfirmation");
                }
                // 有关如何启用帐户确认和密码重置的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=320771
                // 发送包含此链接的电子邮件
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "重置密码", "请通过单击 <a href=\"" + callbackUrl + "\">此处</a>来重置你的密码");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }
        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public virtual ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public virtual ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }
        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // 请不要显示该用户不存在
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }
        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public virtual ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // 请求重定向到外部登录提供程序
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public virtual async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }
        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // 生成令牌并发送该令牌
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }
        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            // 如果用户已具有登录名，则使用此外部登录提供程序将该用户登录
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // 如果用户没有帐户，则提示该用户创建帐户
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }
        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }
            if (ModelState.IsValid)
            {
                // 从外部登录提供程序获取有关用户的信息
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult UserProfile(string userID)
        {
            UserProfile p;
            if (string.IsNullOrWhiteSpace(userID))
            {
                if (User.Identity.IsAuthenticated)
                {
                    ViewBag.Self = true;
                    p = SessionHelper.UserProfile;
                    ViewBag.Profile = p;
                    return View();
                }
                else return RedirectToAction("Login");
            }
            ViewBag.Self = userID == User.Identity.Name;
            ViewBag.Profile = ee.UserProfile.Where(m => m.UserId == userID).SingleOrDefault();
            return View();
        }
        public ActionResult EditProfile()
        {
            var p = SessionHelper.UserProfile;
            //if (p == null || string.IsNullOrWhiteSpace(p.UserId))
            //{
            //    p = new Models.UserProfile();
            //    p.UserId = User.Identity.Name;
            //}
            return View(p);
        }
        List<string> acceptFileType = new List<string>() { ".png", ".jpeg", ".jpg", ".bmp" };
        [HttpPost]
        public ActionResult EditProfile(UserProfile p)
        {
            bool modify = true;
            if (string.IsNullOrWhiteSpace(p.UserId))
            {
                modify = false;
                p.UserId = User.Identity.Name;
            }
            if (ModelState.IsValid)
            {
                var postedFile = Request.Files["Photo"];
                if (!string.IsNullOrWhiteSpace(postedFile.FileName))
                {
                    string filePath;
                    string fileType = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("."));
                    if (!acceptFileType.Contains(fileType))
                    {
                        ViewBag.FileError = "请上传指定的图片文件格式";
                        return View(p);
                    }
                    string fileName;
                    //生成唯一文件名
                    do
                    {
                        fileName = Guid.NewGuid().ToString() + fileType;
                        filePath = CUrl.MapPath(CUrl.UserPhoto + fileName);
                    }
                    while (System.IO.File.Exists(filePath));
                    try
                    {
                        postedFile.SaveAs(filePath);
                        p.Photo = fileName;
                    }
                    catch { }
                }
                if (modify)
                {
                    if (string.IsNullOrWhiteSpace(p.Photo)) p.Photo = SessionHelper.UserProfile.Photo;
                    ee.Entry<UserProfile>(p).State = System.Data.Entity.EntityState.Modified;
                }
                else ee.UserProfile.Add(p);
                ee.SaveChanges();
                SessionHelper.UserProfile = p;
                //ViewBag.Self = true;
                return RedirectToAction("UserProfile");
            }
            return View(p);
            //return View(SessionHelper.UserProfile);
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public virtual ActionResult ExternalLoginFailure()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }
            base.Dispose(disposing);
        }
        #region 帮助程序
        // 用于在添加外部登录名时提供 XSRF 保护
        private const string XsrfKey = "XsrfId";
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }
            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }
            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}