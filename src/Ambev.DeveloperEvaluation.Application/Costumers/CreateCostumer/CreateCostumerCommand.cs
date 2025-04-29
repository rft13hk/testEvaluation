using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;

/// <summary>
/// Command for creating a new Costumer.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a Costumer,
/// including the Costumer name and the ID of the user who created it.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request
/// that returns a <see cref="CreateCostumerResult"/>.
///
/// The data provided in this command is validated using the
/// <see cref="CreateCostumerValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
/// populated and follow the required rules.
/// </remarks>
public class CreateCostumerCommand : IRequest<CreateCostumerResult>
{
    /// <summary>
    /// Gets or sets the name of the Costumer to be created.
    /// </summary>
    public string CostumerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the user who created this Costumer.
    /// </summary>
    public Guid UserId { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateCostumerValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}