using VHS_Tarefas.Entities;
using VHS_Tarefas.Repositories;

namespace VHS_Tarefas.Services
{
    public class ContactChannelService
    {
        private ContactChannelRepository _contactChannelRepository;
        public ContactChannelService(
            ContactChannelRepository contactChannelRepository
            )
        {
            _contactChannelRepository = contactChannelRepository;
        }

        public async Task<ContactChannelEntity> GetOrCreateByPhoneNumber(string phoneNumber)
        {
            var contactChannel = await _contactChannelRepository.GetByPhoneNumber(phoneNumber);
            if(contactChannel != null)
                return contactChannel;

            contactChannel = new ContactChannelEntity();
            contactChannel.PhoneNumber = phoneNumber;

            await _contactChannelRepository.Insert(contactChannel);

            return contactChannel;
        }
    }
}
