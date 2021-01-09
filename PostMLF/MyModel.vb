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
        Console.WriteLine("This line here")
    End Sub

    Public Sub BeforeOptimize() Implements IOpenModel.BeforeOptimize
        'Throw New NotImplementedException()
    End Sub

    Public Sub AfterOptimize() Implements IOpenModel.AfterOptimize
        Dim dMarginalLoss As Double
        Dim dPTDF As Integer
        Dim nPeriod As Integer = 1

        For Each l As Line In G_oPowerFlow.ModeledLines
            dPTDF = l.PTDF(1, 1)
            dMarginalLoss = l.MarginalLossFactor(nPeriod)
        Next
    End Sub

    Public Sub BeforeRecordSolution() Implements IOpenModel.BeforeRecordSolution

        'Dim n As Node
        Dim z As PTDFZone
        Dim nPeriod As Integer = 1
        Dim dPTDF As Integer
        Dim nPath As Integer = 1
        Dim nMap As Integer
        Dim dMarginalLoss As Double
        Dim nInjection As Integer = 1

        'For Each p As Path In G_oPowerFlow.Paths



        '    dMarginalLoss = G_oPowerFlow.MarginalLoss(p.PTDFIndex, nPeriod)




        'Next

        nMap = G_oPowerFlow.NetPTDFMap(nPeriod)

        'For Each n As Node In G_oPowerFlow.InjectionPoints

        '    nPath = 1

        '    nInjection += 1
        'Next

        For Each l As Line In G_oPowerFlow.ModeledLines
            dPTDF = l.PTDF(1, 1)
            dMarginalLoss = l.MarginalLossFactor(nPeriod)
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
