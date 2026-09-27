# SimuladorSO

Simulador de escalonamento de CPU 

## Como funciona

O simulador representa o tempo através de um clock lógico, que avança
conforme os eventos vão sendo processados (chegada de um processo e fim de
execução). Cada processo tem um instante de chegada e uma duração de CPU.

Quando um processo chega, ele entra na fila de prontos. Se a CPU estiver
livre, o processo da frente da fila é despachado e executa até terminar.
Ao final da execução, o próximo processo da fila é despachado, e assim por
diante até que todos os processos tenham sido concluídos.

Durante a execução, o simulador imprime um log de cada evento (chegada e
despacho) no console. Ao final, são exibidas as métricas de cada processo:
tempo de retorno (tempo entre a chegada e a finalização) e tempo de espera
(tempo entre a chegada e o início da execução), além das médias gerais.

## Como executar

A lista de processos está definida diretamente no `Program.cs`. Para rodar:
No Visual Studio, basta abrir `SimuladorSO.sln` e apertar **F5**.

## Estrutura

- `Processo` — representa cada tarefa, com chegada, duração de CPU e os
  instantes de início/fim
- `Evento` — representa algo que acontece em um instante do clock (chegada
  ou fim de execução)
- `Simulador` — controla o clock, a fila de eventos, a fila de prontos e
  imprime o log e as métricas

## Integrantes

Moisés Henrique Campanholo da Silva RA: 114518

Felipe Apolinário de Souza RA: 114771

Bruno Otavio Passini RA: 115660

João Victor Marquesan RA: 114610

Anderson Pantolfi Moraes RA: 114426

Gabriel Vinícius Krebski RA: 115442
