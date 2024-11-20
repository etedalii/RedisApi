
using System.Text.Json;
using StackExchange.Redis;

namespace RedisApi;

public class RedisPlatformRepo : IPlatformRepo
{
    private readonly IConnectionMultiplexer _redis;
    public RedisPlatformRepo(IConnectionMultiplexer multiplexer)
    {
        _redis = multiplexer;
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform == null)
            throw new ArgumentException(nameof(platform));

        var db = _redis.GetDatabase();
        var serialPlatform = JsonSerializer.Serialize(platform);
        db.StringSet(platform.Id, serialPlatform);
        db.SetAdd("PlatformsSet", serialPlatform);
    }

    public Platform? GetPlatformById(string id)
    {
        var db = _redis.GetDatabase();
        var result = db.StringGet(id);
        if(!string.IsNullOrEmpty(result)){
            return JsonSerializer.Deserialize<Platform>(result);
        }
        
        return null;
    }

    public IEnumerable<Platform> GetPlatforms()
    {
        var db = _redis.GetDatabase();
        var completeSets = db.SetMembers("PlatformsSet");
        if(completeSets.Length > 0)
        {
            var obj = Array.ConvertAll(completeSets, val => JsonSerializer.Deserialize<Platform>(val)).ToList();

            return obj;
        }

        return null;
    }
}
