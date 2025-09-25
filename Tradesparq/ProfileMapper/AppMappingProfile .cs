using AutoMapper;
using CrawlService.Dto.Request;
using CrawlService.Dto.Responsive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradesparq.Model.Company;
namespace Tradesparq.ProfileMapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Shipments, SearchResponsiveDto.Doc>()                 // Entity -> DTO
                .ForMember(d => d.id, m => m.MapFrom(s => s.IdShipments))
                .ForMember(d => d.supplier, m => m.MapFrom(s => s.Supplier))
                .ForMember(d => d.buyer, m => m.MapFrom(s => s.Buyer))
                .ForMember(d => d.prod_desc, m => m.MapFrom(s => s.ProdDesc))
                .ForMember(d => d.orig_country_code, m => m.MapFrom(s => s.OrigCountryCode))
                .ForMember(d => d.dest_country_code, m => m.MapFrom(s => s.DestCountryCode))
                .ForMember(d => d.date, m => m.MapFrom(s => s.Date))
                .ForMember(d => d.amount, m => m.MapFrom(s => s.Amount))
                .ForMember(d => d.price, m => m.MapFrom(s => s.Price))
                .ForMember(d => d.weight, m => m.MapFrom(s => s.Weight))
                .ForMember(d => d.quantity, m => m.MapFrom(s => s.Quantity))
                .ForMember(d => d.quantity_unit, m => m.MapFrom(s => s.QuantityUnit))
                .ForMember(d => d.master_bill_no, m => m.MapFrom(s => s.MasterBillNo))
                .ForMember(d => d.container_no, m => m.MapFrom(s => s.ContainerNo))
                .ForMember(d => d.carrier_name, m => m.MapFrom(s => s.CarrierName))
                .ForMember(d => d.vessel_name, m => m.MapFrom(s => s.VesselName))
                .ForMember(d => d.teu, m => m.MapFrom(s => s.Teu))
                .ForMember(d => d.hs_code, m => m.MapFrom(s => s.HsCode))
                .ForMember(d => d.orig_port, m => m.MapFrom(s => s.OrigPort))
                .ForMember(d => d.dest_port, m => m.MapFrom(s => s.DestPort))
                .ForMember(d => d.customs, m => m.MapFrom(s => s.Customs))
                .ForMember(d => d.supplier_id, m => m.MapFrom(s => s.SupplierId))
                .ForMember(d => d.supplier_addr, m => m.MapFrom(s => s.SupplierAddr))
                .ForMember(d => d.exporter_id, m => m.MapFrom(s => s.ExporterId))
                .ForMember(d => d.buyer_id, m => m.MapFrom(s => s.BuyerId))
                .ForMember(d => d.buyer_addr, m => m.MapFrom(s => s.BuyerAddr))
                .ForMember(d => d.importer_id, m => m.MapFrom(s => s.ImporterId))
                .ForMember(d => d.brand, m => m.MapFrom(s => s.Brand))
                .ForMember(d => d.batch_id, m => m.MapFrom(s => s.BatchId))
                .ForMember(d => d.data_source, m => m.MapFrom(s => s.DataSource))
                .ForMember(d => d.incoterms, m => m.MapFrom(s => s.Tncoterms))
                .ForMember(d => d.trans_type, m => m.MapFrom(s => s.TransType))
                .ReverseMap(); 



        }
    }
}
