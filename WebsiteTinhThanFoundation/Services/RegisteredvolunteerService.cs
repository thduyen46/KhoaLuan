using AutoMapper;
using OfficeOpenXml;
using WebsiteTinhThanFoundation.AutoMapperProfiles.Volunteers;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Repository.UnitOfWork;
using WebsiteTinhThanFoundation.Services.Interface;

namespace WebsiteTinhThanFoundation.Services
{
    public class RegisteredvolunteerService : IRegisteredvolunteerService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        private readonly IMapper _mapper;
        public RegisteredvolunteerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AcceptContact(Guid Id)
        {
            var model = await _unitOfWork.RegisteredVolunteerRepository.GetAsync(x => x.Id == Id);
            if(model != null)
            {
                model.IsContacted = true;
                _unitOfWork.RegisteredVolunteerRepository.Update(model);
                await _unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task Add(Registeredvolunteers model)
        {
            model.CreatedOn = DateTime.UtcNow.ToTimeZone();
            _unitOfWork.RegisteredVolunteerRepository.Add(model);
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> CountAsync()
            => await _unitOfWork.RegisteredVolunteerRepository.CountAsync();

        public async Task<ICollection<Registeredvolunteers>> GetAllAsync()
            => (await _unitOfWork.RegisteredVolunteerRepository.GetAllAsync());

        public async Task<ICollection<Registeredvolunteers>> GetNotContactAsync()
        => await _unitOfWork.RegisteredVolunteerRepository.GetAllAsync(x => !x.IsContacted);

        public async Task<Registeredvolunteers?> GetByIdAsync(Guid? Id)
            => await _unitOfWork.RegisteredVolunteerRepository.GetAsync(x => x.Id == Id);

        public async Task<MemoryStream> ExportData()
        {
            await Task.Yield();
            var list = _mapper.Map<List<VolunteerDTO>>(await GetNotContactAsync());
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true, OfficeOpenXml.Table.TableStyles.Light16);
                workSheet.Cells.AutoFitColumns();
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }
    }
}
