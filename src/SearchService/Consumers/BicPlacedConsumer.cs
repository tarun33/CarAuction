using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class BicPlacedConsumer : IConsumer<BidPlaced>
    {
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {

            Console.WriteLine("--> Consuming bid placed");

            var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);

            if( context.Message.BidStatus.Contains("Accepted")
             && context.Message.Amount > auction.CurrentHighBid)
             {
                auction.CurrentHighBid = context.Message.Amount;
                await auction.SaveAsync();
             }
        }
    }
}