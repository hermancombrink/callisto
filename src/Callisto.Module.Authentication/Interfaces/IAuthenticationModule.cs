using Callisto.Module.Authentication.ViewModels;
using Callisto.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Callisto.Module.Authentication.Interfaces
{
    public interface IAuthenticationModule
    {
        Task<RequestResult> RegisterUserAsync(RegisterViewModel model);
    }
}
