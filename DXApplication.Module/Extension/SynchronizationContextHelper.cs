public class SynchronizationContextHelper {
    private ISupportInvokeAsync actionExecutor;
    public void Initialize(ISupportInvokeAsync actionExecutor) {
        this.actionExecutor = actionExecutor;
    }
    public async Task InvokeAsync(Func<Task> action) {
        if (actionExecutor is null) {
            // not initialized yet - throw an error, add to an action queue or handle this case in some other way
        } else {
            await actionExecutor.InvokeAsync(action);
        }
    }
}

public interface ISupportInvokeAsync {
    Task InvokeAsync(Func<Task> action);
}