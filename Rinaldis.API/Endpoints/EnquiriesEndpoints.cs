using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rinaldis.Core.Entities.Enquiry.Commands.CreateEnquiry;
using Rinaldis.Core.Entities.Enquiry.Queries.GetEnquiries;
using Rinaldis.Shared.Dtos.Enquiries;

namespace Rinaldis.API.Endpoints;

public static class EnquiriesEndpoints
{
    public static IEndpointRouteBuilder MapEnquiriesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/enquiries").WithTags("Enquiries");

        group.MapPost("/", CreateEnquiry)
            .WithName("CreateEnquiry")
            .Produces<EnquiryResponse>(StatusCodes.Status201Created)
            .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest);

        group.MapGet("/", GetEnquiries)
            .WithName("GetEnquiries")
            .Produces<List<EnquiryResponse>>();

        return app;
    }

    private static async Task<IResult> CreateEnquiry(
        [FromBody] CreateEnquiryRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.ValidationProblem(new Dictionary<string, string[]>
                { { "Name", new[] { "Name is required." } } });
        if (string.IsNullOrWhiteSpace(request.Email))
            return Results.ValidationProblem(new Dictionary<string, string[]>
                { { "Email", new[] { "Email is required." } } });
        if (string.IsNullOrWhiteSpace(request.Message))
            return Results.ValidationProblem(new Dictionary<string, string[]>
                { { "Message", new[] { "Message is required." } } });

        var command = new CreateEnquiryCommand(
            request.Name.Trim(),
            request.Email.Trim(),
            string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim(),
            request.Message.Trim(),
            request.PreferredDateUtc
        );
        var response = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/enquiries/{response.Id}", response);
    }

    private static async Task<IResult> GetEnquiries(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var list = await mediator.Send(new GetEnquiriesQuery(), cancellationToken);
        return Results.Ok(list);
    }
}
