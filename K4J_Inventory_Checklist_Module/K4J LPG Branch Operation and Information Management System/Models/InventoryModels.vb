' ==========================================================
' INVENTORY AND CHECKLIST MODULE - MODELS AND CONSTANTS
' Plain data classes shared by the data, service and UI layers.
' ==========================================================

''' <summary>Thrown when a business rule is broken. The message is safe to show to the user.</summary>
Public Class BusinessRuleException
    Inherits Exception

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

End Class

''' <summary>Thrown when stock or the checklist changed while the admin was counting.</summary>
Public Class ChecklistChangedException
    Inherits BusinessRuleException

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

End Class

Public Module Roles
    Public Const SuperAdmin As Integer = 1
    Public Const BranchAdmin As Integer = 2
End Module

Public Module StockConditions

    Public Const Filled As String = "FILLED"
    Public Const EmptyCylinder As String = "EMPTY"
    Public Const Revolving As String = "REVOLVING"
    Public Const Damaged As String = "DAMAGED"
    Public Const General As String = "GENERAL"

    Public Function ForCategory(category As String) As String()
        If category = "LPG_CYLINDER" Then
            Return New String() {Filled, EmptyCylinder, Revolving, Damaged}
        End If
        Return New String() {General}
    End Function

    Public Function DisplayName(condition As String) As String
        Select Case condition
            Case Filled
                Return "Filled"
            Case EmptyCylinder
                Return "Empty"
            Case Revolving
                Return "Revolving (field)"
            Case Damaged
                Return "Damaged"
            Case General
                Return "General"
            Case Else
                Return condition
        End Select
    End Function

End Module

Public Module MovementTypes
    Public Const Incoming As String = "INCOMING"
    Public Const Outgoing As String = "OUTGOING"
    Public Const Adjustment As String = "ADJUSTMENT"
End Module

Public Module ChecklistTypes
    Public Const Opening As String = "OPENING"
    Public Const Closing As String = "CLOSING"
End Module

Public Module ChecklistStatuses
    Public Const Draft As String = "DRAFT"
    Public Const Submitted As String = "SUBMITTED"
End Module

Public Class SessionUser
    Public Property UserID As Integer
    Public Property FullName As String
    Public Property Email As String
    Public Property RoleID As Integer
    Public Property BranchID As Integer?
    Public Property BranchName As String

    Public ReadOnly Property IsSuperAdmin As Boolean
        Get
            Return RoleID = Roles.SuperAdmin
        End Get
    End Property

    Public ReadOnly Property IsBranchAdmin As Boolean
        Get
            Return RoleID = Roles.BranchAdmin
        End Get
    End Property
End Class

Public Class ProductInfo
    Public Property ProductID As Integer
    Public Property ProductCode As String
    Public Property ProductName As String
    Public Property Category As String
End Class

Public Class InventoryBalance
    Public Property InventoryID As Integer
    Public Property Quantity As Integer
End Class

Public Class MovementRecord
    Public Property BranchID As Integer
    Public Property ProductID As Integer
    Public Property StockCondition As String
    Public Property MovementType As String
    Public Property QuantityIn As Integer
    Public Property QuantityOut As Integer
    Public Property PreviousBalance As Integer
    Public Property NewBalance As Integer
    Public Property Description As String
    Public Property RecordedBy As Integer
    Public Property ChecklistDetailID As Integer?
End Class

Public Class ChecklistHeader
    Public Property ChecklistID As Integer
    Public Property BranchID As Integer
    Public Property BranchName As String
    Public Property ChecklistType As String
    Public Property ChecklistDate As Date
    Public Property Status As String
    Public Property SubmittedByName As String
    Public Property SubmissionDate As Date?
End Class

Public Class ChecklistLine
    Public Property DetailID As Integer
    Public Property ProductID As Integer
    Public Property ProductName As String
    Public Property StockCondition As String
    Public Property PreviousClosingQuantity As Integer?
    Public Property SystemQuantity As Integer
    Public Property PhysicalQuantity As Integer?
    Public Property Remarks As String
    Public Property Adjusted As Boolean

    ''' <summary>Difference = Physical - System. Negative = missing stock, positive = excess.</summary>
    Public ReadOnly Property Difference As Integer?
        Get
            If PhysicalQuantity.HasValue Then Return PhysicalQuantity.Value - SystemQuantity
            Return Nothing
        End Get
    End Property
End Class

Public Class ChecklistData
    Public Property Header As ChecklistHeader
    Public Property Lines As List(Of ChecklistLine)
End Class

Public Class AdjustmentTarget
    Public Property DetailID As Integer
    Public Property BranchID As Integer
    Public Property ProductID As Integer
    Public Property StockCondition As String
    Public Property Difference As Integer?
    Public Property ChecklistStatus As String
End Class

''' <summary>Item for ComboBox lists (shows Text, carries Value).</summary>
Public Class ChoiceItem
    Public Property Text As String
    Public Property Value As Object

    Public Sub New(text As String, value As Object)
        Me.Text = text
        Me.Value = value
    End Sub

    Public Overrides Function ToString() As String
        Return Text
    End Function
End Class
