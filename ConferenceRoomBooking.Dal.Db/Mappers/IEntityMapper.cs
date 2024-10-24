namespace ConferenceRoomBooking.Dal.Db.Mappers
{
    public interface IEntityMapper<TModel, TEntity>
    {
        TEntity MapToEntity(TModel model);
        TModel MapToModel(TEntity entity);
    }
}
