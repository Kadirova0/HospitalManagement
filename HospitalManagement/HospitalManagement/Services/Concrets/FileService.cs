using HospitalManagement.Contracts;
using HospitalManagement.Services.Abstract;

namespace HospitalManagement.Services.Concrets
{
    public class FileService : IFileService
    {
        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public void Delete(UploadDirectory uploadDir, string fileName)
        {
            throw new NotImplementedException();
        }

        public string Upload(IFormFile file, string path)
        {
            throw new NotImplementedException();
        }

        public string Upload(IFormFile file, UploadDirectory uploadDir)
        {
            throw new NotImplementedException();
        }
    }
}
