<template>
  <el-table :data="employeesData" style="width: 100%">
    <el-table-column label="Name" prop="fullName" />
    <el-table-column label="Date">
      <template #default="{ row }">
        <span>{{ formateDate(row.dateOfBrith) }}</span>
      </template>
    </el-table-column>

    <el-table-column label="Gender">
      <template #default="{ row }">
        <div class="name-wrapper">
          <el-tag>{{ genderText(row.gender) }}</el-tag>
        </div>
      </template>
    </el-table-column>

    <el-table-column label="Department">
      <template #default="{ row }">
        <span>{{ row.department.name }}</span>
      </template>
    </el-table-column>

    <el-table-column align="right">
      <template #header>
        <div class="search-block">
          <el-input v-model="search" size="mini" placeholder="Type to search" />
          <el-button
            @click="addOrEditDraer = true"
            class="search-btn"
            type="primary"
            plain
            >Add</el-button
          >
        </div>
      </template>
      <template #default="scope">
        <el-button size="mini" @click="handleEdit(scope.$index, scope.row)"
          >Edit</el-button
        >

        <el-popconfirm
          @confirm="handleDelete(scope.$index, scope.row)"
          title="Are you sure to delete this?"
        >
          <template #reference>
            <el-button size="mini" type="danger">Delete</el-button>
          </template>
        </el-popconfirm>
      </template>
    </el-table-column>
  </el-table>

  <add-or-edit-employee
    v-if="addOrEditDraer"
    :employee="employee"
    @closeDrawer="closeDrawer"
  ></add-or-edit-employee>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import { showMessage } from "@/common/commonMixin.js";
import AddOrEditEmployee from "./AddOrEditEmployee.vue";
export default {
  components: { AddOrEditEmployee },
  mixins: [showMessage],
  data() {
    return {
      employee: null,
      addOrEditDraer: false,
      search: "",
    };
  },
  computed: {
    ...mapGetters(["employees"]),
    employeesData() {
      return this.employees.filter(
        (data) =>
          !this.search ||
          data.fullName.toLowerCase().includes(this.search.toLowerCase())
      );
    },
  },
  methods: {
    ...mapActions(["getAllEmployees", "getAllDepartments", "deletEmployee"]),
    handleEdit(index, row) {
      this.employee = row;
      this.addOrEditDraer = true;
    },
    closeDrawer() {
      this.employee = null;
      this.addOrEditDraer = false;
    },
    async handleDelete(index, row) {
      let result = await this.deletEmployee(row.id);
      if (result.success) {
        this.showMessage("Employee Deleted", "success");
      }
    },
    formateDate(date) {
      var options = {
        weekday: "long",
        year: "numeric",
        month: "long",
        day: "numeric",
      };
      var today = new Date(date);
      return today.toLocaleDateString("en-US", options);
    },
    genderText(gender) {
      let genderText = "";
      switch (gender) {
        case 0:
          genderText = "Male";
          break;
        case 1:
          genderText = "Female";
          break;
        case 2:
          genderText = "Other";
          break;

        default:
          break;
      }
      return genderText;
    },
  },
  async created() {
    await this.getAllDepartments();
    let result = await this.getAllEmployees();
    if (result && result.msg) {
      this.showMessage(result.msg, "error");
    }
  },
};
</script>

<style scoped>
.search-block {
  display: flex;
}
.search-btn {
  margin-left: 10px;
}
</style>