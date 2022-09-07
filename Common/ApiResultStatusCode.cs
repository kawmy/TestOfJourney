using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "Done")]
        Success = 0,

        [Display(Name = "Server Error")]
        ServerError = 1,

        [Display(Name = "Bad Params")]
        BadRequest = 2,

        [Display(Name = "Not Found")]
        NotFound = 3,

        [Display(Name = "List is empty")]
        ListEmpty = 4,

        [Display(Name = "Pros Error")]
        LogicError = 5,

        [Display(Name = "Auth Error")]
        UnAuthorized = 6
    }
}
