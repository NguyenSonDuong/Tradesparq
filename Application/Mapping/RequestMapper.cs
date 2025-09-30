using Application.Dto.ResponsiveDto;
using AutoMapper;
using CrawlService.Dto.Responsive;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<SearchCompanyResponsivDto.Bucket, Company>()
               .ForMember(d => d.Address, o => o.MapFrom(s => s.info.address.Trim()))
               .ForMember(d => d.Country, o => o.MapFrom(s => s.info.country.Trim()))
               .ForMember(d => d.Name, o => o.MapFrom(s => s.info.name.Trim()))
               .ForMember(d => d.Uid, o => o.MapFrom(s => s.info.uid.Trim()))
               .ForMember(d => d.Uname, o => o.MapFrom(s => s.info.uname.Trim()))
               .ForMember(d => d.Uuid, o => o.MapFrom(s => s.info.uuid.Trim()))
               .ForMember(d => d.Total, o => o.MapFrom(s => s.total))
               .ForMember(d => d.Count, o => o.MapFrom(s => s.count))
               .ForMember(d => d.Var, o => o.MapFrom(s => s.val.Trim()));

            CreateMap<SearchResponsiveDto.Doc, Shipment>()
                .ForMember(d => d.Id, m => m.Ignore())
                // string (trim & null-safe)
                .ForMember(d => d.IdShipments, m => m.MapFrom(s => SafeStringNonNull(s.id)))
                .ForMember(d => d.BatchId, m => m.MapFrom(s => SafeString(s.batch_id)))
                .ForMember(d => d.DataSource, m => m.MapFrom(s => SafeString(s.data_source)))
                .ForMember(d => d.Date, m => m.MapFrom(s => SafeString(s.date)))
                .ForMember(d => d.SupplierId, m => m.MapFrom(s => SafeString(s.supplier_id)))
                .ForMember(d => d.Supplier, m => m.MapFrom(s => SafeString(s.supplier)))
                .ForMember(d => d.SupplierAddr, m => m.MapFrom(s => SafeString(s.supplier_addr)))
                .ForMember(d => d.ExporterId, m => m.MapFrom(s => SafeString(s.exporter_id)))
                .ForMember(d => d.BuyerId, m => m.MapFrom(s => SafeString(s.buyer_id)))
                .ForMember(d => d.Buyer, m => m.MapFrom(s => SafeString(s.buyer)))
                .ForMember(d => d.BuyerAddr, m => m.MapFrom(s => SafeString(s.buyer_addr)))
                .ForMember(d => d.ImporterId, m => m.MapFrom(s => SafeString(s.importer_id)))   // object? -> string?
                .ForMember(d => d.HsCode, m => m.MapFrom(s => SafeString(s.hs_code)))
                .ForMember(d => d.ProdDesc, m => m.MapFrom(s => SafeString(s.prod_desc)))
                .ForMember(d => d.OrigCountryCode, m => m.MapFrom(s => SafeString(s.orig_country_code)))
                .ForMember(d => d.DestCountryCode, m => m.MapFrom(s => SafeString(s.dest_country_code)))
                .ForMember(d => d.OrigPort, m => m.MapFrom(s => SafeString(s.orig_port)))
                .ForMember(d => d.DestPort, m => m.MapFrom(s => SafeString(s.dest_port)))
                .ForMember(d => d.Customs, m => m.MapFrom(s => SafeString(s.customs)))
                .ForMember(d => d.QuantityUnit, m => m.MapFrom(s => SafeString(s.quantity_unit)))
                .ForMember(d => d.MasterBillNo, m => m.MapFrom(s => SafeString(s.master_bill_no)))
                .ForMember(d => d.ContainerNo, m => m.MapFrom(s => SafeString(s.container_no)))
                .ForMember(d => d.TransType, m => m.MapFrom(s => SafeString(s.trans_type)))
                .ForMember(d => d.CarrierName, m => m.MapFrom(s => SafeString(s.carrier_name)))
                .ForMember(d => d.VesselName, m => m.MapFrom(s => SafeString(s.vessel_name)))

                // số: map trực tiếp double? hoặc parse an toàn từ object?
                .ForMember(d => d.Teu, m => m.MapFrom(s => SafeDouble(s.teu)))
                .ForMember(d => d.Amount, m => m.MapFrom(s => SafeDouble(s.amount)))
                .ForMember(d => d.Price, m => m.MapFrom(s => SafeDouble(s.price)))
                .ForMember(d => d.Weight, m => m.MapFrom(s => SafeDouble(s.weight)))
                .ForMember(d => d.Quantity, m => m.MapFrom(s => SafeDouble(s.quantity)))
                .ForMember(d => d.Tncoterms, m => m.MapFrom(s => SafeDouble(s.incoterms)))  // object? -> double?
                .ForMember(d => d.Brand, m => m.MapFrom(s => SafeDouble(s.brand)));     // object? -> double?
        }
        // --- helpers ---
        private static string? SafeString(string? s) =>
            string.IsNullOrWhiteSpace(s) ? null : s.Trim();

        private static string SafeStringNonNull(string? s) =>
            string.IsNullOrWhiteSpace(s) ? string.Empty : s.Trim();

        private static string? SafeString(object? o)
            => o is null ? null : SafeString(Convert.ToString(o, CultureInfo.InvariantCulture));

        private static double? SafeDouble(double? v)
            => v.HasValue && !double.IsNaN(v.Value) && !double.IsInfinity(v.Value) ? v : null;

        private static double? SafeDouble(object? o)
        {
            if (o is null) return null;
            switch (o)
            {
                case double d when !double.IsNaN(d) && !double.IsInfinity(d): return d;
                case float f when !float.IsNaN(f) && !float.IsInfinity(f): return (double)f;
                case decimal de: return (double)de;
                case long l: return l;
                case int i: return i;
                case string s when double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var val): return val;
                default: return null;
            }
        }
    }
}
