using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<CategoryResponse>>
{
    private readonly ITypesRepository _typesRepository;

    public GetAllTypesHandler(ITypesRepository typesRepository)
    {
        _typesRepository = typesRepository;
    }
    public async Task<IList<CategoryResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typesList = await _typesRepository.GetAllTypes();
        var categoryResponseList = ProductMapper.Mapper.Map<IList<CategoryResponse>>(typesList);
        return categoryResponseList;
    }
}