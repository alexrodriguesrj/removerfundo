# removerfundo

Esta biblioteca DLL em C# permite remover o fundo de imagens usando o modelo U-2-Net em formato ONNX.

## ✨ Funcionalidades

- Remoção de fundo com IA usando modelo ONNX (ex: U-2-Net)
- Retorno da imagem com fundo transparente (formato PNG)
- Integrável em aplicações web ou desktop no Windows

## 🚀 Como usar

1. Baixe ou converta o modelo ONNX U-2-Net e salve na pasta do projeto.
2. Instale os pacotes via NuGet (Microsoft.ML.OnnxRuntime, OpenCvSharp).
3. Instancie a classe `BackgroundRemover` e use o método `RemoverFundo`:

```csharp
var remover = new BackgroundRemover("u2net.onnx");
byte[] resultado = remover.RemoverFundo(File.ReadAllBytes("foto.jpg"));
File.WriteAllBytes("foto_sem_fundo.png", resultado);
```

## 🧠 Modelo ONNX

Você pode baixar o modelo U-2-Net ONNX aqui:  
https://huggingface.co/briaai/RMBG-1.4

## 📦 Estrutura da DLL

- `BackgroundRemover.cs`: lógica principal
- `Utils.cs`: conversão de imagem, máscara e aplicação do alpha

## 🛠 Requisitos

- .NET 7
- OpenCvSharp
- Microsoft.ML.OnnxRuntime

## 📝 Licença

MIT
