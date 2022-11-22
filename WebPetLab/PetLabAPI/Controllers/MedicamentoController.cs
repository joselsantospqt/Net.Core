using Domain.Entidade;
using Domain.Entidade.Request;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        private MedicamentoService _ServiceMedicamento;
        public MedicamentoController(MedicamentoService serviceExame)
        {
            _ServiceMedicamento = serviceExame;
        }


        [HttpGet("getAll")]
        //[Authorize]
        public ActionResult GetAll()
        {
            var getAllPet = _ServiceMedicamento.GetAll();

            return Ok(getAllPet);
        }



        [HttpGet("{id:Guid}")]
        //[Authorize]
        public ActionResult GetById([FromRoute] Guid id)
        {

            var medicamento = _ServiceMedicamento.GetMedicamentoById(id);

            if (medicamento == null)
                return NoContent();

            return Ok(medicamento);
        }


        [HttpGet("{idProntuario:Guid}")]
        public ActionResult GetMedicamentoByProntuarioId([FromRoute] Guid id)
        {
            var medicamentos = _ServiceMedicamento.GetAll().Where(x => x.Prontuario.ProntuarioId == id);

            if (medicamentos == null)
                return NoContent();

            return Ok(medicamentos);
        }



        [HttpPost]
        public ActionResult Medicamento([FromRoute] Guid idProntuario, [FromBody] CreateMedicamento medicamentoCreate)
        {

            var medicamento = _ServiceMedicamento.CreateMedicamento(idProntuario, medicamentoCreate.Codigo, medicamentoCreate.Nome, medicamentoCreate.Quantidade, medicamentoCreate.Data_Inicio, medicamentoCreate.Data_Fim);

            return Created("api/[controller]", medicamento);
        }


        [HttpDelete("{id:Guid}")]
        //[Authorize]
        public ActionResult Delete(Guid id)
        {

            _ServiceMedicamento.DeleteMedicamento(id);

            return NoContent();
        }


        [HttpPut("{id:Guid}")]
        //[Authorize]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Medicamento update)
        {
            Medicamento medicamentoUpdate = update;
            medicamentoUpdate.Id = id;
            var updateMedicamento = _ServiceMedicamento.UpdateMedicamento(medicamentoUpdate);

            return Ok(updateMedicamento);

        }
    }
}
