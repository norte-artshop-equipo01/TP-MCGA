using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
       
        protected void CheckAuditPattern(IdentityBase model, bool created = false)
        {
            string userId = TryGetUserId();
            if (created)
            {
                model.CreatedOn = DateTime.Now;
                model.CreatedBy = userId;
            }
            model.ChangedOn = DateTime.Now;
            model.ChangedBy = userId;
        }
        protected virtual string TryGetUserId()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            string userId = null;
            try
            {
                userId = User.Identity.Name;
                if (userId != null)
                    return userId;
            }
            catch { /* no action */}

            return userId;
        }
    }
}