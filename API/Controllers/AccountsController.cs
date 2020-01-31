using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Accounts.Commands.CreateAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountsController : BaseController
    {
        /// <summary>
        /// Creates an Account
        /// </summary> 
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Account
        ///     {
        ///       "customerId": "af2d09ed-7bc6-4921-aae7-76a93920a497",
        ///       "accountName": "juan dela cruz",
        ///       "accountNumber": "109451902655"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">returns the newly generated Account Guid</response>
        /// <response code="400">if there's an invalid field request</response>  
        /// <response code="500">returns if there's any unhandled request.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create(CreateAccountCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}