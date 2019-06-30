using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingUpload.Models;

namespace TrainingUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadedFilesController : ControllerBase
    {
        private readonly UploadedFileContext _context;

        public UploadedFilesController(UploadedFileContext context)
        {
            this._context = context;
        }

        // GET: api/UploadFile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UploadedFileDetails>>> GetPaymentDetails()
        {
            return await _context.UploadedFileDetailsList.ToListAsync();
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                await Task.Delay(1000);

                UploadedFileDetails details = null;

                var file = Request.Form.Files[0];
                string fileName = "";
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine("UploadedFiles", fileName);
                    details = new UploadedFileDetails
                    {
                        Name = fileName,
                        Path = fullPath
                    };

                    _context.UploadedFileDetailsList.Add(details);
                    await _context.SaveChangesAsync();

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok(details);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _context.UploadedFileDetailsList.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            _context.UploadedFileDetailsList.Remove(file);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}