using System.ComponentModel.DataAnnotations;
using Furion.ConfigurableOptions;

namespace Ecdmin.Web.Core.Options
{
    public class AppSettingsOptions : IConfigurableOptions
    {
        [Required]
        public string UploadPath { get; set; }
    }
}