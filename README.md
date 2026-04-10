# W I P
> Alguns phonemizers estão passando por ajustes. Você é livre para dar commit.
# Phonemizers, C#, OpenUtau e demais informações técnicas
Repositório público de Phonemizers feitos por Xiao Pingguo.

[![Compile OpenUtau Phonemizers](https://github.com/dorayakito/Phonemizers-por-Xiao/actions/workflows/compile-phonemizers.yml/badge.svg)](https://github.com/dorayakito/Phonemizers-por-Xiao/actions/workflows/compile-phonemizers.yml)

# Phonemizers para OpenUtau (PT-BR)

Olá! Este repositório foi criado para ajudar a comunidade de UTAU brasileira a ter mais opções de phonemizers no OpenUtau. Aqui você encontrará uma coleção de plugins focados no nosso idioma, cobrindo desde sistemas tradicionais até os mais modernos.

> [!IMPORTANT]
> **Um aviso importante:** Estes arquivos estão em formato de código-fonte (`.cs`). Isso significa que eles não são programas que você apenas baixa e clica duas vezes. Para usá-los, você precisará "compilar" (transformar o código em programa) junto com o OpenUtau. Não se preocupe, os passos abaixo vão te guiar nesse processo!

## O que tem aqui dentro?

Aqui eu faço upload dos phonemizers que fiz e quero compartilhar com todos. C# é o linguagem de programação que me acompanha há mais de 10 anos e quero tornar tudo isso o mais acessível. Já é acessível mas muitas pessoas que não conhecem lógica de programação ou demais conceitos sentem-se perdidas. Aqui trago uma série de opções para diferentes tipos de voicebanks:

1. **BRAPA CVVC**: Suporte ao sistema CVVC utilizando o Alfabeto Fonético Brasileiro (BRAPA).
2. **BRAPA VCV**: Suporte ao sistema VCV utilizando a fonética BRAPA.
3. **BRAPA CV**: Suporte ao sistema CV simples utilizando a fonética BRAPA.
4. **IPEA CVVC**: Implementação do sistema IPÊ-A com conversão G2P (Grafema para Fonema) integrada.
5. **X-SAMPA CVVC**: Suporte ao padrão internacional X-SAMPA aplicado ao Português Brasileiro.
6. **F-SAMPA/BRIGADEIRO**: Suporte à notação simplificada F-SAMPA e ao sistema Brigadeiro.
7. **NUBLADO**: Suporte ao sistema NUBLADO com aliases envoltos em parênteses `( )` e fonemas específicos.
8. **CATIPA**: Suporte ao sistema CATIPA baseado no voicebank Junei Aiden, com fonemas estaleiros e nasais específicos (`a'`, `a~`, etc.).
9. **CATIPA DiffSinger**: Versão para DiffSinger do sistema CATIPA, para ser usada com modelos DIFF.
10. **X-SAMPA DiffSinger**: Versão para DiffSinger do sistema X-SAMPA, para ser usada com modelos DIFF.

Além disso, este pacote inclui documentação técnica:

- [Guia Técnico do Phonemizer: Explicação detalhada da estrutura do código.](https://github.com/dorayakito/Phonemizers-por-Xiao/blob/main/phonemizer_guide.md)
- [Introdução ao C#: O que é a linguagem e suas aplicações.](https://github.com/dorayakito/Phonemizers-por-Xiao/blob/main/csharp_intro.md)

## Instalação

### Pré-requisitos

- .NET SDK (pacotes da Microsoft para desenvolvimento C#).
- Git instalado (opcional, para clonar o repositório).

### Passos para Compilação

1. Faça o clone do código-fonte do OpenUtau:

   ```bash
   git clone https://github.com/stakira/OpenUtau.git
   ```

2. Abra a pasta do OpenUtau fonte.

3. Copie os arquivos `.cs` deste repositório para a pasta `OpenUtau.Core/Plugins` (ou `Plugins` na raiz do projeto).

4. Abra o terminal na pasta raiz do projeto.

5. Execute a compilação:

   ```bash
   dotnet build
   ```

6. Execute o OpenUtau:

   ```bash
   dotnet run --project OpenUtau
   ```

Alternativamente, os phonemizers podem ser carregados automaticamente se colocados na pasta `Plugins` de uma instalação do OpenUtau que suporte carregamento de scripts C#.

## Uso

1. No OpenUtau, selecione a trilha do cantor desejado.
2. Nas configurações da trilha (Track Settings), localize a opção "Phonemizer".
3. Selecione o phonemizer correspondente ao sistema de gravação e fonética do seu voicebank (ex: `PT-BR CVVC BRAPA`).
4. Insira as letras nas notas. Os phonemizers converterão automaticamente os textos para os aliases correspondentes no voicebank.

## Sistemas Fonéticos Suportados

### BRAPA

Baseado na tabela oficial do Team-BRAPA. Utiliza fonemas como `a, ax, eh, oh, an, en, in, on, un` para vogais e `sh, ch, dji, chi` para consoantes específicas.

### IPEA

Utiliza a notação IPÊ-A com suporte a vogais como `@, 7, 1, 0, Q`. Inclui lógica de G2P para facilitar a inserção de letras convencionais.

### X-SAMPA

Segue a convenção X-SAMPA para PT-BR, utilizando símbolos como `E, O, a~, e~, i~, o~, u~, S, Z, J, L`.

### F-SAMPA

Notação simplificada para rapidez na escrita e compatibilidade com voicebanks do sistema Brigadeiro.

### NUBLADO

Sistema caracterizado por aliases em parênteses `( )`. Utiliza fonemas como `a], e], i], o], u]` para nasais, `3` para "é" e `0` para "ó". Inclui suporte a finalizações especiais como `aR` e `awn`.

### CATIPA

Baseado no sistema do voicebank Junei Aiden. Utiliza apóstrofos para vogais reduzidas (`a'`, `e'`) e til para nasais (`a~`, `e~`). Inclui consoantes como `dj`, `tch`, `rr` e `wr`.

## Requisitos

- OpenUtau v0.5 ou superior.
- Código-fonte do OpenUtau para compilação dos plugins.
- Pacotes da Microsoft (SDK do .NET, etc.) devidamente instalados para o processo de compilação.
- Voicebank configurado com oto.ini compatível com os respectivos sistemas.

---

# Conhecendo a Linguagem C#

Você deve ter notado que todos os meus arquivos terminam em `.cs`. Isso acontece porque eles foram escritos em **C#** (lê-se "C-Sharp"). Mas o que exatamente é isso? Vamos bater um papo sobre essa tecnologia que dá vida aos phonemizers do OpenUtau.

## Afinal, o que é C#?
Imagine que o C# é o idioma que usamos para dar ordens ao computador. Ele é uma linguagem de programação moderna desenvolvida pela **Microsoft** e é uma das favoritas no mundo todo por ser poderosa e, ao mesmo tempo, organizada.

### Principais Características:
- **Sintaxe Familiar**: Se parece muito com Java, C e C++, o que facilita o aprendizado para quem já conhece essas linguagens.
- **Gerenciamento de Memória**: Possui um "Garbage Collector" (Coletor de Lixo) que limpa a memória automaticamente, evitando travamentos por falta de espaço.
- **Segurança de Tipos**: O compilador impede erros comuns, como tentar somar um texto com um número de forma indevida.
- **Multiplataforma**: Graças ao .NET moderno, o código escrito em C# pode rodar no Windows, Linux, macOS, Android e iOS.

## Origem e Evolução
O C# surgiu no ano **2000**, desenvolvido por uma equipe da **Microsoft** liderada por **Anders Hejlsberg** (o mesmo criador do Turbo Pascal e do Delphi). 

A linguagem nasceu como parte da estratégia .NET da Microsoft para competir com o Java da Sun Microsystems. Desde então, ela evoluiu de uma linguagem exclusiva para Windows para uma das linguagens mais versáteis e amadas do mundo, sendo hoje um projeto de código aberto.

## Aplicações do C#
O C# é uma linguagem "pau para toda obra". Veja onde ela é mais usada:

1. **Desenvolvimento de Jogos**: É a linguagem principal da **Unity**, a engine de jogos mais popular do mundo (usada em jogos como *Cuphead*, *Hollow Knight* e *Genshin Impact*).
2. **Aplicações Desktop**: Praticamente todos os softwares robustos de Windows (como o próprio **OpenUtau**) são feitos em C#.
3. **Sistemas Web**: Com o **ASP.NET Core**, é usada para criar sites e APIs de alta performance (usados por empresas como Stack Overflow).
4. **Aplicativos Móveis**: Através do **MAUI/Xamarin**, permite criar apps para Android e iPhone usando o mesmo código.
5. **Automação e Plugins**: Como no caso do OpenUtau, serve para estender as funcionalidades de outros programas através de plugins (DLLs).

---

### Por que o OpenUtau usa C#?
O OpenUtau é construído sobre o framework **Avalonia** e o ecossistema **.NET**, o que o torna extremamente rápido e capaz de rodar em diferentes sistemas operacionais. Usar C# para os phonemizers permite que os plugins tenham acesso direto às funções internas do programa com máxima performance.

# Como funciona um Phonemizer?

Se você já se perguntou o que acontece "debaixo do capô" quando você digita uma letra no OpenUtau e ele toca um som, este guia é para você. Vamos explorar como os nossos arquivos `.cs` são organizados e o que cada parte faz.

## 1. O Ponto de Partida: Importações (Using)

No topo do arquivo, as diretivas `using` importam as bibliotecas necessárias para o funcionamento do plugin.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using OpenUtau.Api;
```

- **System**: Funcionalidades básicas da linguagem C#.
- **System.Collections.Generic**: Para o uso de dicionários e listas (essenciais para mapear fonemas).
- **System.Linq**: Utilizado para operações de ordenação (ex: priorizar fonemas mais longos).
- **OpenUtau.Api**: **Crucial**. Contém as definições do OpenUtau para que o plugin possa interagir com o software.

## 2. Atributo do Plugin (Metadata)

Esta linha avisa ao OpenUtau que esta classe é um plugin de phonemizer e define como ele aparecerá no programa.

```csharp
[Phonemizer("Portuguese BRAPA Phonemizer", "PT-BR BRAPA", "xiao")]
```

- **Nome**: O nome que aparece na lista de seleção do OpenUtau.
- **Tag**: Uma sigla curta usada internamente.
- **Autor**: Identificação do criador do plugin (neste projeto, definido como `xiao`).

## 3. Definição da Classe

A classe deve herdar de `Phonemizer` para ter acesso aos métodos e propriedades base do OpenUtau.

```csharp
public class MeuPhonemizer : Phonemizer {
    // Código aqui
}
```

## 4. Campos e Mapeamentos (Dictionaries)

Aqui definimos as tabelas de conversão que dizem ao plugin como interpretar o texto.

```csharp
private readonly string[] vowels = { "a", "e", "i", "o", "u" };
private readonly Dictionary<string, string> g2p = new Dictionary<string, string> {
    { "á", "a" },
    { "ã", "an" },
    { "ch", "sh" }
};
```

- **Vowels/Consonants**: Listas de sons reconhecidos pelo sistema.
- **G2P (Grapheme-to-Phoneme)**: Um "tradutor" que converte o texto digitado no fonema interno correspondente.

## 5. O Método Core: `Process`

Este é o coração do phonemizer. Ele é chamado pelo OpenUtau toda vez que uma nota é processada para decidir qual som tocar.

### Parâmetros do `Process`

```csharp
public override Result Process(Note[] notes, Note? prev, Note? next, Note? prevNeighbour, Note? nextNeighbour, Note[] prevNeighbours)
```

- **notes**: A nota atual (e informações de duração/tom).
- **prevNeighbour**: A nota anterior que contém som (importante para transições VC).

### Lógica de Busca de Oto (oto.ini)

Para phonemizers tradicionais, verificamos se o alias existe no arquivo `oto.ini` do cantor:

```csharp
if (singer.TryGetMappedOto(alias, tone, out var oto)) {
    // Se encontrou, adiciona o fonema à lista
    phonemes.Add(new Phoneme { phoneme = alias });
}
```

## Diferença entre Phonemizer Tradicional e DiffSinger (DIFF)

- **Tradicional**: Precisa verificar cada alias no arquivo `oto.ini` do cantor (`TryGetMappedOto`), pois depende de gravações físicas (CVVC, VCV).
- **DiffSinger**: Não faz checagem de `oto.ini`. Ele apenas "devolve" a sequência de fonemas puros, pois o motor de IA do DiffSinger sabe como sintetizar os sons de forma fluida a partir da fonética pura.

