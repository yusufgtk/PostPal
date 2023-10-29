using SocialMedia.entity;

namespace SocialMedia.data.Abstract
{
    public interface IBlueTickApplicationRepository:IRepository<BlueTickApplication>
    {
        List<BlueTickApplication> GetBlueTickApplications();
        BlueTickApplication GetBlueTickApplicationById(int id);
        BlueTickApplication GetBlueTickApplicationByUserId(string userId);
        void DeleteReferences();// belli kriterlerin dışındaki başvuruları direkt siler
    }
}