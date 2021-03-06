namespace OJS.Web.Areas.Contests.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using OJS.Data;
    using OJS.Web.Areas.Contests.ViewModels;
    using OJS.Web.Controllers;

    public class ContestsController : BaseController
    {
        public ContestsController(IOjsData data)
            : base(data)
        {
        }

        public ActionResult Details(int id)
        {
            var contestViewModel = this.Data.Contests.All()
                                                .Where(x => x.Id == id)
                                                .Select(ContestViewModel.FromContest)
                                                .FirstOrDefault();

            if (contestViewModel == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Could not find a contest with this id.");
            }

            return this.View(contestViewModel);
        }

        public ActionResult BySubmissionType(int id)
        {
            var contests = this.Data.Contests
                                            .All()
                                            .Where(x => x.SubmissionTypes.Any(s => s.Id == id))
                                            .Select(ContestViewModel.FromContest);

            return this.View(contests);
        }
    }
}
