namespace ConferenceRoomBooking.Dal.Db
{
    public interface IEntityMapper<TModel, TEntity>
    {
        TEntity MapToEntity(TModel model);
        TModel MapToModel(TEntity entity);
    }
}
