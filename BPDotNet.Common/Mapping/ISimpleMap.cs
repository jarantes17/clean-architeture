namespace BPDotNet.Common.Mapping;

public interface ISimpleMap<in TSource, out TTarget>
{
    TTarget Map(TSource source);
}