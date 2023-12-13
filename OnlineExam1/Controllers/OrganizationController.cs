using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam1.DTO;
using OnlineExam1.Entity;
using OnlineExam1.Repo;
namespace OnlineExam1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrganizationController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(MyContext context, ILogger<OrganizationController> logger)
        {
            unitOfWork = new UnitOfWork(context);
            _logger = logger;
        }

        
        [HttpGet, Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<OrganizationDTO>> GetOrganizations()
        {
            try
            {
                var organizations = unitOfWork.OrganizationRepoImplObject.GetAll();
                return Ok(organizations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }

       
        [HttpGet, Route("GetById{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OrganizationDTO> GetOrganization(int id)
        {
            try
            {
                var organization = unitOfWork.OrganizationRepoImplObject.GetById(id);

                if (organization == null)
                {
                    return NotFound();
                }

                return Ok(new OrganizationDTO
                {
                    Id = organization.OrgID,
                    Name = organization.OrgName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }
        [HttpGet, Route("GetByName/{name}")]
        [Authorize(Roles = "User")]
        public ActionResult<OrganizationDTO> GetOrganizationByName(string name)
        {
            try
            {
                var organization = unitOfWork.OrganizationRepoImplObject.GetByName(name);

                if (organization == null)
                {
                    return NotFound();
                }

                return Ok(new OrganizationDTO
                {
                    Id = organization.OrgID,
                    Name = organization.OrgName
                });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost, Route("Add")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OrganizationDTO> PostOrganization(OrganizationDTO organizationDTO)
        {
            try
            {
                unitOfWork.OrganizationRepoImplObject.Add(organizationDTO);
                unitOfWork.SaveAll();

                return CreatedAtAction(nameof(GetOrganization), new { id = organizationDTO.Id }, organizationDTO);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

       
        [HttpPut, Route("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutOrganization(int id, OrganizationDTO organizationDTO)
        {
            try
            {
                if (id != organizationDTO.Id)
                {
                    return BadRequest();
                }

                bool success = unitOfWork.OrganizationRepoImplObject.Update(id, organizationDTO);

                if (!success)
                {
                    return NotFound();
                }

                unitOfWork.SaveAll();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }

        
        [HttpDelete, Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteOrganization(int id)
        {
            try
            {
                bool success = unitOfWork.OrganizationRepoImplObject.Delete(id);

                if (!success)
                {
                    return NotFound();
                }

                unitOfWork.SaveAll();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }
    }
}