using System;
using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TRex.Metadata;

namespace LogicAppUtilites.Controllers
{
    public class DateController : ApiController
    {
        /// <summary>
        ///     Converts DateTime to double
        /// </summary>
        /// <param name="value">Universal DateTime</param>
        /// <returns></returns>
        [Metadata("Converts Universal DateTime to number")]
        [SwaggerResponse(HttpStatusCode.OK, "The operation was successful", typeof (long))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "DateTime is invalid")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerOperation(nameof(DateTimeToNumber))]
        public long DateTimeToNumber(
            [Metadata("value", "DateTime value")] DateTime value)
        {
            var localTime1 = DateTime.SpecifyKind(value, DateTimeKind.Local);
            DateTimeOffset dateTimeOffset = localTime1;

            var unixTimeStampInMilliseconds = dateTimeOffset.ToUnixTimeMilliseconds();

            return unixTimeStampInMilliseconds;
        }

        /// <summary>
        ///     Convert Number to DateTime
        /// </summary>
        /// <param name="unixTimeStamp">"Unix DateTime as double</param>
        /// <returns>DateTime</returns>
        [Metadata("Converts number to  Universal DateTime")]
        [SwaggerResponse(HttpStatusCode.OK, "The operation was successful", typeof (DateTime))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Number is invalid DateTime")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerOperation(nameof(ConvertUnixTimeStampToDateTime))]
        public DateTime ConvertUnixTimeStampToDateTime(
            [Metadata("number", "Unix TimeStamp")] long unixTimeStamp)
        {
            var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp);

            var dateTime = dateTimeOffset.DateTime;
            return dateTime;
        }
    }
}