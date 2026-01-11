using System.Reflection;

namespace ControleDeFrete.Infrastructure;


public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof( AssemblyReference ).Assembly;
}