using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("BepisAPI/[controller]")]
    [ApiController]
    public class AwesomeCalculatorController : ControllerBase
    {
        [HttpGet("Calculate")]
        public ActionResult Calculate(byte CalcType, int CountA, int CountB)
        {
            // ,==================================================,
            // |                   Calc type                      ,
            // |           0 = Additive - a + b = c               ,
            // |         1 = Subtractive - a - b = c              ,
            // |           2 = Multiply - a x b = c               ,
            // |          3 = Division = a / b = c                ,
            // '=================================================='
            switch (CalcType)
            {
                case 0:
                    return Ok(CountA + CountB);
                case 1:
                    return Ok(CountA - CountB);
                case 2:
                    return Ok(CountA * CountB);
                case 3:
                    return Ok(Math.Round((decimal)(CountA / CountB), 10));
                default:
                    return Ok("Bad denominator!!! Must be from 0 to 3.\n0 is add, 1 is subtract,\n2 is multiply & 3 is division.\n\n(Don't ask why the system is like this...)");
            }
        }
    }
}
