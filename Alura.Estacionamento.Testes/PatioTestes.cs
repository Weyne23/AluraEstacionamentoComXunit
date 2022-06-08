using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes : IDisposable
    {
        public ITestOutputHelper _saidaConsoleTeste;
        private readonly Veiculo _veiculo;
        private readonly Patio _patio;
        private readonly Operador _operador;

        public PatioTestes(ITestOutputHelper saidaConsoleTeste)
        {
            _saidaConsoleTeste = saidaConsoleTeste;
            _saidaConsoleTeste.WriteLine("Construtor invocado.");
            _veiculo = new Veiculo();
            _patio = new Patio();
            _operador = new Operador();
            _operador.Nome = "Pedro Fagundes";
            _patio.OperadorPatio = _operador;
        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoDoVeiculo()
        {
            //Arrange
            _veiculo.Proprietario = "André Silva";
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = "Verde";
            _veiculo.Modelo = "Fusca";
            _veiculo.Placa = "ASD-9999";

            _patio.RegistrarEntradaVeiculo(_veiculo);
            _patio.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act
            double faturamento = _patio.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);

        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        [InlineData("José Silva", "POL-9242", "Cinza", "Fusca")]
        [InlineData("Maria Silva", "GDR-6524", "Azul", "Opala")]
        [InlineData("Pedro Silva", "RSD-4315", "Fosco", "Corsa")]
        public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string proprietario,
                                                       string placa,
                                                       string cor,
                                                       string modelo)
        {
            //Arrenge
            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;

            _patio.RegistrarEntradaVeiculo(_veiculo);
            _patio.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act
            double faturamento = _patio.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);

        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        public void LocalizaVeiculoPatioComBaseNoIdTicket(string proprietario,
                                                       string placa,
                                                       string cor,
                                                       string modelo)
        {
            //Arrenge
            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;

            _patio.RegistrarEntradaVeiculo(_veiculo);

            //Act
            var consultado = _patio.PesquisaVeiculo(_veiculo.IdTicket);

            //Assert
            Assert.Contains("### Ticket Estacionamento Alura ###" +
                            $">>> Identificador: {_veiculo.IdTicket}" +
                            $">>> Data/Hora de Entrada: {DateTime.Now}" +
                            $">>> Placa Veiculo: {_veiculo.Placa}" +
                            $">>> Operador Patio: {_patio.OperadorPatio.Nome}", _veiculo.Ticket);
        }

        [Fact]
        public void AlteraDadosVeiculoDoPropioVeiculo()
        {
            _veiculo.Proprietario = "André Silva";
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = "Verde";
            _veiculo.Modelo = "Fusca";
            _veiculo.Placa = "ASD-9999";

            _patio.RegistrarEntradaVeiculo(_veiculo);

            _veiculo.Cor = "Preto";

            //Act
            var alterado = _patio.AlterardadosVeiculo(_veiculo);

            //Assert
            Assert.Equal(alterado.Cor, _veiculo.Cor);
        }

        public void Dispose()
        {
            _saidaConsoleTeste.WriteLine("Dispose invocado.");
        }
    }
}
