namespace Infrastructure.ServiceInterfaces
{
    public interface IPixelCollidable : ICollidable2D
    {
        // PixelCollisionManager PixelCollisionManager { get; set; }
        void OnPixelsCollision(ICollidable2D i_Source);

    }
}
