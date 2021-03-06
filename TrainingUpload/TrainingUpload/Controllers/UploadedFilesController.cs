﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingUpload.Interfaces;
using TrainingUpload.Models;
using TrainingUpload.Persistence;
using TrainingUpload.Utilities;

namespace TrainingUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadedFilesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public UploadedFilesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UploadedFileDetails>> GetFileDetails()
        {
            var files = unitOfWork.FileRepository.GetAll();

            return Ok(files);
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                await Task.Delay(1000);
                var file = Request.Form.Files[0];

                var uploadedDetails = FileHandler.SaveFile(file);

                unitOfWork.FileRepository.Add(uploadedDetails);
                unitOfWork.Commit();

                return Ok(uploadedDetails);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
         public IActionResult DeleteFile(int id)
        {
            try
            {
                var file = unitOfWork.FileRepository.SingleOrDefault(a => a.Id == id);

                if (file == null)
                {
                    return NotFound();
                }

                if (System.IO.File.Exists(file.Path))
                {
                    System.IO.File.Delete(file.Path);
                }

                unitOfWork.FileRepository.Remove(file);
                unitOfWork.Commit();

                return Ok(file);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); ;
            }
            
        }
    }
}