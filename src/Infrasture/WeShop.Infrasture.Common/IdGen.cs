using Snowflake.Core;

namespace WeShop.Infrasture.Common
{
    /// <summary>
    /// Id生成器
    /// </summary>
    public static class IdGen
    {
        private static readonly IdWorker IdWorker = new IdWorker(1, 1);

        public static long Create()
        {
            var id = IdWorker.NextId();
            return id;
        }
    }
}