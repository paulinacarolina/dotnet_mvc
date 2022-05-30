using Xunit;

namespace XUnit.Coverlet.MSBuild;

public class UnitTest1
{
    double numero = 3.0;

    [Fact]
    public void TestNumber()
    {
        Assert.IsType<int>(ParseDoubleToInt(numero));
    }

    #region funciones 
    public int ParseDoubleToInt(double numero)
    {
        return (int)numero;
    }
    #endregion
}