# FOFO_Dev
<img src="game.png" alt="game">

> Corra contra o tempo e contra o bot colete moedas mais rápido que o iimigo, pontue e quebre seus recordes!

### Assets usados
Grid e pathfinding - https://arongranberg.com/astar/download

3d Model e animações - https://assetstore.unity.com/packages/3d/animations/basic-motions-free-154271#publisher

## Pré-requisitos
Se seu sistema operacional estiver entre Windows, Mac ou Linux, você pode executar o arquivo FOFO_Dev.exe dentro da pasta build.

## Metodologia
Foram usados dois patterns padrões no projeto. O primeiro deles é o MVC, consiste em dividir as funcionalidades em 3 subtipos de classe:
  
  ● Data: Go to application > model > ...
  ● Logic/Workflow: Go to application > controller > ...
  ● Rendering/Interface/Detection: Go to application > view > ..

O segundo pattern usado foi o "Object Pooling". Esse pattern consiste em não destruir objetos, mas sim, reutilizá-los no deccorer do game. Foi aplicado na moeda, evitando desperdícios de memória em destruição e criação de novos objetos.

## Gameplay settings
A cena está dividida em alguns componentes lógicos. irei fazer uma lista e especificar cada um e quais informações carregam:
  ● MVC
   ● Model: 
      Em model temos dois objetos, um deles carrega dados de score como current score e high score. O segundo objeto carrega dados do game, como o tempo de partida.         Alterando esse tempo, você mudaria o tempo que o jogo ocorre. No padrão, começamos em 60 segundos.
   ● View:
      Em view, outros dois objetos, um deles com as informações de interface do score/highscore e o segundo com as informações de timer, que servem para mostrar o           tempo de partida restante.
   ● Controller:
      Em controller, também são dois objetos. O primeiro segue o padrão de informações sobre o score, porém ele tem as referências de model e view, além de dois             eventos públicos que são usados para adicionar pontos e atualizar o placar de highscore ao fim do jogo. O segundo objeto é o game controller, ele tem as               referências de game view e model, além de ter uma referência de score controller, o game controller é o controlador principal da partida, verifica tempo e             encerra o jogo quando necessário.
