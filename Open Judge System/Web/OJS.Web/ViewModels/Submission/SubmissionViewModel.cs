﻿namespace OJS.Web.ViewModels.Submission
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using OJS.Data.Models;
    using OJS.Web.ViewModels.TestRun;
using System.Linq.Expressions;

    public class SubmissionViewModel
    {
        public static Expression<Func<Submission, SubmissionViewModel>> FromSubmission
        {
            get
            {
                return submission => new SubmissionViewModel
                {
                    Id = submission.Id,
                    SubmitedOn = submission.CreatedOn,
                    ProblemId = submission.ProblemId,
                    ProblemName = submission.Problem.Name,
                    ProblemMaximumPoints = submission.Problem.MaximumPoints,
                    Contest = submission.Problem.Contest.Name,
                    ParticipantId = submission.ParticipantId,
                    ParticipantName = submission.Participant.User.UserName,
                    TestResults = submission.TestRuns.AsQueryable().Select(TestRunViewModel.FromTestRun)
                };
            }
        }

        public int Id { get; set; }

        public int? ParticipantId { get; set; }

        public string ParticipantName { get; set; }

        public DateTime SubmitedOn { get; set; }

        public int? ProblemId { get; set; }

        public string ProblemName { get; set; }

        public int ProblemMaximumPoints { get; set; }

        public string Contest { get; set; }

        public string ProgrammingLanguage { get; set; }

        public IEnumerable<TestRunViewModel> TestResults { get; set; }

        public int Points
        {
            get
            {
                var correctTests = (decimal)TestResults
                    .Where(x => x.ExecutionResult == TestRunResultType.CorrectAnswer)
                    .Count();

                var allTests = TestResults.Count();

                return (int)((correctTests / allTests) * this.ProblemMaximumPoints);
            }
        }

        public bool HasFullPoints
        {
            get
            {
                return this.Points == 100 ? true : false;
            }
        }

        public int MaxUsedTime
        {
            get
            {
                return TestResults
                    .Select(x => x.TimeUsed)
                    .Max();
            }
        }

        public long MaxUsedMemory
        {
            get
            {
                return TestResults
                    .Select(x => x.MemoryUsed)
                    .Max();
            }
        }
    }
}