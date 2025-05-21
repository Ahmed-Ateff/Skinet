using API.RequestHelpers;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePageResult<T>(IGenericRepository<T> repo,
            ISpecification<T> spec,int pageSize,int pageIndex)where T:BaseEntity
        {
            var items = await repo.ListAsync(spec);
            var count=await repo.CountAsync(spec);
            var pagination = new Pagination<T>(pageIndex,pageSize,count,items);
            return Ok(pagination);
        }
    }
}
