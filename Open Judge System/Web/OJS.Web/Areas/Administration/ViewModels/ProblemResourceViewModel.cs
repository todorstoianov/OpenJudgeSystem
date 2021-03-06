﻿namespace OJS.Web.Areas.Administration.ViewModels
{
    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using OJS.Data.Models;

    public class ProblemResourceViewModel
    {
        public static Expression<Func<ProblemResource, ProblemResourceViewModel>> FromProblemResource
        {
            get
            {
                return res => new ProblemResourceViewModel
                {
                    Id = res.Id,
                    Name = res.Name,
                    Type = res.Type,
                    OrderBy = res.OrderBy,
                    ProblemId = res.ProblemId,
                    ProblemName = res.Problem.Name,
                };
            }
        }

        public int? Id { get; set; }

        [Display(Name = "Име")]
        [Required(ErrorMessage = "Името е задължително!", AllowEmptyStrings = false)]
        [MinLength(3)]
        [MaxLength(50)]
        [DefaultValue("Име")]
        public string Name { get; set; }

        public int ProblemId { get; set; }

        public string ProblemName { get; set; }

        [Display(Name = "Тип")]
        [Required(ErrorMessage = "Типа е задължителен!")]
        [DefaultValue(ProblemResourceType.ProblemDescription)]
        public ProblemResourceType Type { get; set; }

        public IEnumerable<SelectListItem> AllTypes { get; set; }

        public HttpPostedFileBase File { get; set; }

        public string FileExtension
        {
            get
            {
                var fileName = this.File.FileName;
                return fileName.Substring(fileName.LastIndexOf('.') + 1);
            }
        }

        [Display(Name = "Линк")]
        [DefaultValue("http://")]
        public string Link { get; set; }

        [Display(Name = "Подредба")]
        [Required(ErrorMessage = "Подредбата е задължителна!")]
        public int OrderBy { get; set; }
    }
}