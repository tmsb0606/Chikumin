using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public static class LeanTweenAsyncExtensions
{
    //public static async Task AwaitCompletionAsync(this LTDescr tween)
    //{
    //    var tcs = new TaskCompletionSource<bool>();
    //    tween.setOnComplete(() => tcs.TrySetResult(true));
    //    await tcs.Task;
    //}

    // sample -----
    //public async Task AsyncStartTween(CancellationToken cancellationToken = default)
    //{
    //    var tween = LeanTween.value(this.gameObject, 0, 1, 5).setOnUpdate((v) => {
    //        this.transform.localScale = Vector3.one + Vector3.one * v * 2;
    //    });
    //    await tween.AwaitCompletionAsync(cancellationToken);
    //    DebugOnGUI.LogWarning("Complete");
    //}

    public static async Task AwaitCompletionAsync(this LTDescr tween, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<bool>();
        tween.setOnComplete(() => tcs.TrySetResult(true));

        using (cancellationToken.Register(() => {
            LeanTween.cancel(tween.uniqueId);
            tcs.TrySetCanceled();
        }))
        {
            await tcs.Task;
        }
    }

    public static async Task DelayedCall(float delayTime, CancellationToken cancellationToken = default)
    {
        var tween = LeanTween.delayedCall(delayTime, () => { });
        await tween.AwaitCompletionAsync(cancellationToken);
    }
}
