using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class Shipment
    {
        public int Id { get; set; }
        public string IdShipments { get; set; }
        public string? BatchId { get; set; }
        public string? DataSource { get; set; }
        public string? Date { get; set; }
        public string? SupplierId { get; set; }
        public string? Supplier { get; set; }
        public string? SupplierAddr { get; set; }
        public string? ExporterId { get; set; }
        public string? BuyerId { get; set; }
        public string? Buyer { get; set; }
        public string? BuyerAddr { get; set; }
        public string? ImporterId { get; set; }
        public string? HsCode { get; set; }
        public string? ProdDesc { get; set; }
        public string? OrigCountryCode { get; set; }
        public string? DestCountryCode { get; set; }
        public string? OrigPort { get; set; }
        public string? DestPort { get; set; }
        public string? Customs { get; set; }
        public double? Teu { get; set; }
        public double? Amount { get; set; }
        public double? Price { get; set; }
        public double? Weight { get; set; }
        public double? Quantity { get; set; }
        public string? QuantityUnit { get; set; }
        public string? MasterBillNo { get; set; }
        public string? ContainerNo { get; set; }
        public string? TransType { get; set; }
        public double? Tncoterms { get; set; }
        public string? CarrierName { get; set; }
        public string? VesselName { get; set; }
        public double? Brand { get; set; }
    }
}
