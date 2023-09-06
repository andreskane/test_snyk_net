namespace ABI.API.Structure.HealthChecks
{
    //public class ApiTruckHealthCheck : IHealthCheck
    //{
    //    public Task<HealthCheckResult> CheckHealthAsync(
    //        HealthCheckContext context,
    //        CancellationToken cancellationToken = default(CancellationToken))
    //    {

    //        var apiTruck = new ApiTruck();

    //        var status = apiTruck.GetStatusApi();


    //        if (status != null && !string.IsNullOrEmpty(status.Static))
    //        {
    //            var dataStatus = string.Format("Api Truck healthy. Status:{0}, DateTime: {1}", status.Static, status.Date);


    //            return Task.FromResult(
    //                HealthCheckResult.Healthy(dataStatus));
    //        }




    //        var dataStatusError = string.Format("Api Truck unhealthy. Status: ERROR, DateTime: {0}", DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt"));

    //        return Task.FromResult(
    //          HealthCheckResult.Unhealthy(dataStatusError));

    //        return Task.FromResult(
    //            HealthCheckResult.Healthy(""));

    //    }
    //}
}
