
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.Dtos;
using Application.Customers.Queries.GetCustomer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CustomersController : BaseController
    {

        /// <summary>
        /// Get the details of all customer
        /// </summary> 
        /// <response code="200">returns if the record has been successfully retrieved.</response>
        /// <response code="400">returns if there's an invalid field request</response>
        /// <response code="500">returns if there's any unhandled request.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetCustomersDto>> GetCustomers()
        {
            return await Mediator.Send(new GetCustomersQuery());
        }

        /// <summary>
        /// Get the details of a specific customer
        /// </summary> 
        /// <response code="200">returns if the record has been successfully retrieved.</response>
        /// <response code="400">returns if there's an invalid field request</response>
        /// <response code="500">returns if there's any unhandled request.</response>
        /// <param name="id">The specific Guid to be used for getting the Customer details.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetCustomerDto>> GetSpecificCustomer(Guid id)
        {
            return await Mediator.Send(new GetCustomerQuery { Id = id });
        }

        /// <summary>
        /// Creates a Customer
        /// </summary> 
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Customers
        ///     {
        ///       "firstName": "juan",
        ///       "lastName": "dela cruz",
        ///       "age": 18,
        ///       "emailAddress": "test@gmail.com"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">returns the newly generated Customer Guid</response>
        /// <response code="400">if there's an invalid field request</response>  
        /// <response code="500">returns if there's any unhandled request.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create(CreateCustomerCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Updates a Customer
        /// </summary> 
        /// <response code="204">returns if the customer has been successfully updated</response>
        /// <response code="400">returns if there's an invalid field request</response>
        /// <response code="500">returns if there's any unhandled request.</response>
        /// <param name="id">The specific Guid of the customer to be used for updating its details.</param>
        /// <param name="command">The customer fields to be updated.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid id, UpdateCustomerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes a Customer
        /// </summary> 
        /// <response code="204">returns if the customer has been successfully deleted</response>
        /// <response code="400">returns if there's an invalid field request</response>
        /// <response code="500">returns if there's any unhandles request.</response>
        /// <param name="id">The specific Guid to be used for deleting a Customer.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteCustomerCommand { Id = id });

            return NoContent();
        }

    }
}
