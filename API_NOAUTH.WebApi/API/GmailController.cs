using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DXApplication.Module.BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Security.AccessControl;

namespace API_NOAUTH.WebApi.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomProfileController : ControllerBase
    {
        IObjectSpaceFactory objectSpaceFactory;
        public CustomProfileController(IObjectSpaceFactory objectSpaceFactory)
        {
            this.objectSpaceFactory = objectSpaceFactory;
        } 
        [HttpPut("{profileId}")]
        public IActionResult UpdateProfile(string profileId)
        {
            
            IObjectSpace objectSpace = objectSpaceFactory.CreateObjectSpace(typeof(Profile));

            Profile existing = objectSpace.GetObjects<Profile>().Where(p => p.Id == profileId).FirstOrDefault();

            if (existing == null)
            {
                return NotFound();
            }
            if(existing.Gmail != null) {
                existing.Gmail.Status = GmailStatus.Dead;
            }
           
            objectSpace.CommitChanges();
            Random random = new Random();
            Gmail gmail = objectSpace.GetObjects<Gmail>()
                                    .Where(x => x.ProfileId == null && x.Status == GmailStatus.Live)
                                    .OrderBy(x => random.Next())
                                    .FirstOrDefault();
            if (gmail == null)
            {
                return NotFound();
            }
            existing.Gmail = gmail;
            existing.Gmail.ProfileId = profileId;
            objectSpace.CommitChanges();

            return Ok(new
            {
                email = existing.Gmail.Email,
                password = existing.Gmail.Password,
            });
           
            
        }
        
    }
}
