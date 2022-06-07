using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class VeiculoTestes : IDisposable
    {
        public ITestOutputHelper _saidaConsoleTeste;
        private readonly Veiculo _veiculo;

        public VeiculoTestes(ITestOutputHelper saidaConsoleTeste)
        {
            _saidaConsoleTeste = saidaConsoleTeste;
            _saidaConsoleTeste.WriteLine("Construtor invocado.");
            _veiculo = new Veiculo();
        }

        [Fact]
        public void TestaVeiculoAcelerarComParametro10()
        {
            //Arrange
            //var veiculo = new Veiculo();
            //Act
            _veiculo.Acelerar(10);
            //Assert
            Assert.Equal(100, _veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaVeiculoFreiarComParametro15()
        {
            //Arrange
            //var veiculo = new Veiculo();
            //Act
            _veiculo.Frear(15);
            //Assert
            Assert.Equal(-225, _veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaTipoVeiculoAutomovel()
        {
            //Arrange
            //var veiculo = new Veiculo();
            //Act
            //Assert
            Assert.Equal(TipoVeiculo.Automovel, _veiculo.Tipo);
        }

        [Fact(DisplayName = "Validar Nome do Proprietario", Skip = "Teste ainda não implementado.")]
        public void ValidaNomeProprietarioDoVeiculo()
        {

        }

        [Fact]
        public void FichaDeInformacaoDoVeiculos()
        {
            //Arrange
            //var veiculo = new Veiculo();
            _veiculo.Proprietario = "André Silva";
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = "Verde";
            _veiculo.Modelo = "Fusca";
            _veiculo.Placa = "ASD-9999";

            //Act
            string dados = _veiculo.ToString();

            //Assert
            Assert.Contains("Ficha do Veículo", dados);
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDe3Caracteres()
        {
            //Arrange
            string nomeProprietario = "Ae";

            //Assert
            Assert.Throws<FormatException>(
                    //Act
                    () => new Veiculo(nomeProprietario)
                );
        }

        [Fact]
        public void TestaMensagemDeExcecaoDoQuartoCaractereDaPlaca()
        {
            //Arrange
            string placa = "ASDF8888";

            //Assert
            var mensagem = Assert.Throws<FormatException>(
                                    //Act
                                    () => new Veiculo().Placa = placa
                                );

            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
        }

        [Fact]
        public void TestaMensagemDeExcecaoSeUltimosCaracteresDaPlacaSaoNumeros()
        {
            //Arrange
            string placa = "ASD-S88S";

            //Assert
            var mensagem = Assert.Throws<FormatException>(
                                    //Act
                                    () => new Veiculo().Placa = placa
                                );

            Assert.Equal("Do 5º ao 8º caractere deve-se ter um número!", mensagem.Message);
        }

        public void Dispose()
        {
            _saidaConsoleTeste.WriteLine("Dispose invocado.");
        }
    }
}
