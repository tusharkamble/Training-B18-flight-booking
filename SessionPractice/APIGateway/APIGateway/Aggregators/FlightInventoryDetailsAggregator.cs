using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using APIGateway.Models;

using Microsoft.AspNetCore.Http;

using Ocelot.Middleware;
//using Ocelot.Multiplexer; //not found ...need to reinstall latest version



namespace APIGateway.Aggregators
{
    public class FlightInventoryDetailsAggregator //: IDefinedAggregator
    {
        //public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        //{
        //    var resultingAggregation = new ResultingAggregation<List<Object>>();

        //    HttpResponseMessage response = new HttpResponseMessage();

        //    try
        //    {
        //        var book = await responses[0].Items.DownstreamResponse().Content.ReadAsAsync<List<Object>>();

        //        try
        //        {
        //            var price = await responses[1].Items.DownstreamResponse().Content.ReadAsAsync<List<Object>>();

        //            book.Price = price.Price;

        //            resultingAggregation.Ok(book);
        //        }
        //        catch (Exception)
        //        {
        //            resultingAggregation.Partial(book, "There was an error when loading Book DownStreams.");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        resultingAggregation.Error("There was error when loading the book details.");
        //    }

        //    response.Content = new ObjectContent<ResultingAggregation<List<Object>>>(resultingAggregation, new JsonMediaTypeFormatter());

        //    return new DownstreamResponse(response);
        //}
    }
}