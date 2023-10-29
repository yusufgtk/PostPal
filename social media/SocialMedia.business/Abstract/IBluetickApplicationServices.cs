using SocialMedia.business.Abstract;
using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface IBlueTickApplicationServices
    {
        void Create(BlueTickApplication entity);
        void Delete(BlueTickApplication entity);
        void Update(BlueTickApplication entity);
        List<BlueTickApplication> GetBlueTickApplications();
        BlueTickApplication GetBlueTickApplicationById(int id);
        BlueTickApplication GetBlueTickApplicationByUserId(string userId);
        void DeleteReferences();// belli kriterlerin dışındaki başvuruları direkt siler
    }
}