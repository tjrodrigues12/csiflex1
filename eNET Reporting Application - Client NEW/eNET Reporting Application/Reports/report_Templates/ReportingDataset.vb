Partial Public Class ReportingDataset
    Partial Class tbl_partsNumberDataTable

        Private Sub tbl_partsNumberDataTable_tbl_partsNumberRowChanging(sender As Object, e As tbl_partsNumberRowChangeEvent) Handles Me.tbl_partsNumberRowChanging

        End Sub

        Private Sub tbl_partsNumberDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.partNameColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class Tbl_History18DataTable

        Private Sub Tbl_History18DataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.WeekNumberColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
