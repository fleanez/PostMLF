Imports EEPHASE
Imports EEUTILITY.Enums

Public Class MyModel
    Implements EEPHASE.IOpenModel

    Public Sub AfterInitialize() Implements IOpenModel.AfterInitialize
        'Throw New NotImplementedException()
    End Sub

    Public Sub BeforeProperties() Implements IOpenModel.BeforeProperties
        'Throw New NotImplementedException()
    End Sub

    Public Sub AfterProperties() Implements IOpenModel.AfterProperties
        'Throw New NotImplementedException()
    End Sub

    Public Sub BeforeOptimize() Implements IOpenModel.BeforeOptimize
        'Throw New NotImplementedException()
    End Sub

    Public Sub AfterOptimize() Implements IOpenModel.AfterOptimize
        'Throw New NotImplementedException()
    End Sub

    Public Sub BeforeRecordSolution() Implements IOpenModel.BeforeRecordSolution

        Dim nPeriod As Integer
        Dim PTDF() As Single
        Dim nZones As Integer
        Dim dMarginalLoss As Double
        Dim nInjIndex As Integer

        'Debug prints:
        'Console.Write("Points:" & G_oPowerFlow.InjectionPoints.Count)
        'Console.Write("Zones:" & G_oPowerFlow.PTDFZones.Count)
        'For Each z As PTDFZone In G_oPowerFlow.PTDFZones
        '    nInjIndex = z.PTDFIndex
        '    Console.WriteLine("Zone" & nInjIndex & z.Name)
        'Next

        nZones = G_oPowerFlow.PTDFZones.Count
        For nPeriod = 1 To CurrentModel.Steps.StepPeriodCountSansLookahead

            Dim dPenaltyFactors As Double() = Enumerable.Repeat(1.0, nZones).ToArray

            For Each line As Line In G_oPowerFlow.ModeledLines
                dMarginalLoss = line.MarginalLoss(nPeriod)
                PTDF = line.PTDFsforPeriod.Invoke(nPeriod)

                Debug.Assert(nZones = PTDF.Length, "Why these two are different?")

                nInjIndex = 0
                For Each dPTDF As Single In PTDF
                    dPenaltyFactors(nInjIndex) -= dPTDF * dMarginalLoss
                    nInjIndex += 1
                Next
                'Console.WriteLine(line.Name & ":" & dMarginalLoss)
            Next

            For Each trafo As Transformer In G_oPowerFlow.ModeledTransformers
                dMarginalLoss = trafo.MarginalLoss(nPeriod)
                PTDF = trafo.PTDFsforPeriod.Invoke(nPeriod)

                Debug.Assert(nZones = PTDF.Length, "Why these two are different?")

                nInjIndex = 0
                For Each dPTDF As Single In PTDF
                    dPenaltyFactors(nInjIndex) -= dPTDF * dMarginalLoss
                    nInjIndex += 1
                Next
                'Console.WriteLine(line.Name & ":" & dMarginalLoss)
            Next

            Dim nIndex As Integer
            For Each n As Node In G_oPowerFlow.InjectionPoints
                nIndex = n.PTDFZone.PTDFIndex
                n(SystemNodesEnum.x, nPeriod) = n.Price(nPeriod) * dPenaltyFactors(nIndex)
                n(SystemNodesEnum.z, nPeriod) = dPenaltyFactors(nIndex)
            Next
        Next

    End Sub

    Public Sub AfterRecordSolution() Implements IOpenModel.AfterRecordSolution
        'Throw New NotImplementedException()
    End Sub

    Public Sub TerminatePhase() Implements IOpenModel.TerminatePhase
        'Throw New NotImplementedException()
    End Sub

    Public Function OnWarning(Message As String) As Boolean Implements IOpenModel.OnWarning
        'Throw New NotImplementedException()
    End Function

    Public Function EnforceMyConstraints() As Integer Implements IOpenModel.EnforceMyConstraints
        'Throw New NotImplementedException()
    End Function

    Public Function HasDynamicTransmissionConstraints() As Boolean Implements IOpenModel.HasDynamicTransmissionConstraints
        'Throw New NotImplementedException()
    End Function
End Class
