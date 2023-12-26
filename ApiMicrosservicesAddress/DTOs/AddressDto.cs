using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ApiMicrosservicesAddress.DTOs;
public partial record AddressDto
{
    [GeneratedRegex("[^0-9a-zA-Z]")]
    private static partial Regex MyRegex();

    private string _cep;

    [Key]
    [Required]
    [JsonPropertyName("cep")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "Máximo e minimo de 8 dígitos.")]
    public string Cep
    {
        get => _cep;
        set => _cep = MyRegex().Replace(value, "");
    }

    [JsonPropertyName("logradouro")]
    public string Logradouro { get; set; } = string.Empty;

    [JsonPropertyName("complemento")]
    public string Complemento { get; set; } = string.Empty;

    [JsonPropertyName("bairro")]
    public string Bairro { get; set; } = string.Empty;

    [JsonPropertyName("localidade")]
    public string Localidade { get; set; } = string.Empty;

    [JsonPropertyName("uf")]
    public string Uf { get; set; } = string.Empty;

    [JsonPropertyName("ibge")]
    public string Ibge { get; set; } = string.Empty;

    [JsonPropertyName("gia")]
    public string Gia { get; set; } = string.Empty;

    [JsonPropertyName("ddd")]
    public string Ddd { get; set; } = string.Empty;

    [JsonPropertyName("siafi")]
    public string Siafi { get; set; } = string.Empty;

    public AddressDto(string cep, string logradouro, string complemento, string bairro, string localidade, string uf, string ibge, string gia, string ddd, string siafi)
    {
        Cep = cep;
        Logradouro = logradouro;
        Complemento = complemento;
        Bairro = bairro;
        Localidade = localidade;
        Uf = uf;
        Ibge = ibge;
        Gia = gia;
        Ddd = ddd;
        Siafi = siafi;
    }
}
