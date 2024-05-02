using Common.Server.Test;
using Maple.Db;
using S = Maple.I18n.S;

namespace Maple.Eps.Test;

public class CounterTests : IDisposable
{
    private readonly RpcTestRig<MapleDb, Api.Api> _rpcTestRig;

    public CounterTests()
    {
        _rpcTestRig = new RpcTestRig<MapleDb, Api.Api>(S.Inst, MapleEps.Eps, c => new Api.Api(c));
    }

    [Fact]
    public async Task Counter_Success()
    {
        var (ali, _, _) = await _rpcTestRig.NewApi("ali");
        var aliSes = await ali.Auth.GetSession();
        var (bob, _, _) = await _rpcTestRig.NewApi("bob");
        var bobSes = await bob.Auth.GetSession();
        var counter = await ali.Counter.Get(new(aliSes.Id));
        Assert.Equal(0u, counter.Value);
        counter = await ali.Counter.Increment();
        Assert.Equal(1u, counter.Value);
        counter = await ali.Counter.Decrement();
        Assert.Equal(0u, counter.Value);
        counter = await ali.Counter.Decrement();
        Assert.Equal(0u, counter.Value);
        counter = await bob.Counter.Get(new(aliSes.Id));
        Assert.Equal(0u, counter.Value);
        await bob.Auth.FcmEnabled(new(true));
        await bob.Auth.FcmRegister(new(new List<string>() { aliSes.Id }, "abc", null));
        counter = await ali.Counter.Increment();
        Assert.Equal(1u, counter.Value);
    }

    public void Dispose()
    {
        _rpcTestRig.Dispose();
    }
}
