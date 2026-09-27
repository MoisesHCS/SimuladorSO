# Simulador Simplificado (FCFS) — versão didática

Versão mínima só pra entender o mecanismo de um simulador de eventos
discretos: **clock lógico avançando de evento em evento**, nunca em tempo
real. Cada evento pode gerar novos eventos futuros.

## Como rodar

Não precisa de argumento nenhum — só apertar **F5** no Visual Studio, ou:

```bash
dotnet run
```

A carga de exemplo está fixa dentro do `Program.cs` (4 processos). Pra testar
outro cenário, edite a lista `processos` no fim do arquivo.

## O que tem

- Um clock lógico (`_clock`) que só avança quando um evento é processado
- Uma fila de eventos simples (lista + ordenação por tempo e sequência)
- Uma fila de prontos (FCFS: primeiro que chega, primeiro que executa)
- Log de cada evento no console
- Métricas no final: tempo de retorno e tempo de espera por processo

## O que foi cortado (de propósito, pra simplificar)

Essa versão **não cumpre os requisitos da entrega intermediária**. Ela serve
só como ponto de partida pra entender a lógica antes de estudar a versão
completa (pasta `SOSimulator/`). Ficou de fora:

- Separação entre Processo e Thread (não tem PCB/TCB)
- Mais de um surto de CPU por processo / operações de E/S
- Round Robin e Prioridades (só tem FCFS)
- Custo de troca de contexto
- Carga de trabalho em arquivo externo (está fixa no código)
- Testes automatizados

## Próximo passo

Depois de entender esse arquivo único, dá uma olhada no projeto completo em
`SOSimulator/` — ele tem a mesma ideia central (clock + fila de eventos),
só que separado em classes/módulos e com os requisitos da entrega
intermediária (PCB/TCB, threads, as 3 políticas de escalonamento,
testes automatizados, carga de trabalho externa).
