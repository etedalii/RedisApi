﻿namespace RedisApi;

public interface IPlatformRepo
{
    void CreatePlatform(Platform platform);
    Platform? GetPlatformById(string id);
    IEnumerable<Platform> GetPlatforms();
}
