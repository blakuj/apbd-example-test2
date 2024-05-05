using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repository;

namespace WebApplication2.Controllers;


[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly GroupRepository _groupRepository;
    public GroupController(GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetGroup(int id)
    {
        if (!await _groupRepository.doesGroupExist(id))
        {
            return NotFound($"Group with given id: {id} does not exist");
        }

        var group = await _groupRepository.getGroup(id);

        return Ok(group);

    }
}